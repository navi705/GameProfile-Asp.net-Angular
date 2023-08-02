using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameProfile.Infrastructure.Steam;
using System.Security.Claims;
using GameProfile.Application.CQRS.Profiles.Commands;
using GameProfile.Application;
using GameProfile.Persistence.Caching;
using GameProfile.Application.CQRS.Profiles.Requests.GetBySteamId;
using GameProfile.Domain.Enums.Profile;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Commands;
using GameProfile.Application.CQRS.Games.Requests.GetGameByName;
using GameProfile.Application.CQRS.Games.NotSteamGameAppID.Requests;
using GameProfile.WebAPI.ApiCompilation;
using static GameProfile.Infrastructure.Steam.Models.ListGames;

namespace GameProfile.WebAPI.Controllers
{
    public sealed class AuthenticationController : ApiController
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly ISteamApi _steamApi;
        private readonly SteamApiCompilation _steamApiCompilation;
        public AuthenticationController(ISender sender, ICacheService cacheService, ILogger<AuthenticationController> logger, ISteamApi steamApi) : base(sender)
        {
            _cacheService = cacheService;
            _logger = logger;
            _steamApi = steamApi;
            _steamApiCompilation = new(sender,steamApi);
        }

        [AllowAnonymous]
        [HttpPost("login/steam")]
        public async Task<IActionResult> LoginBySteam(SteamOpenIdData steamOpenIdData)
        {
            //SteamApi steamApi = new();
            string idUser = steamOpenIdData.openidclaimed_id[37..];
            if (await _steamApi.CheckOpenIdSteam(steamOpenIdData) == false)
            {
                _logger.LogInformation("Auth not correct");
                return Unauthorized();
            }

            var games = await _steamApi.SteamOwnedGames(idUser);
            if (games.games is null)
            {
                _logger.LogInformation($"Profile is close {idUser}");
                return BadRequest("Check your profile settings");
            }

            var userInfo = await _steamApi.SteamUserGetPlayerSummaries(idUser);

            var query = new GetProfileQuery(idUser);
            var profile = await Sender.Send(query);

            if (profile is null)
            {
                var query2 = new CreateProfileCommand(userInfo[1], "", idUser);
                await Sender.Send(query2);
                profile = await Sender.Send(query);
                _logger.LogInformation($"Profile is added Id-{idUser} NickName-{userInfo[1]}");
                foreach (var item in games.games)
                {
                    //if(item.appid == 115320)
                    //{
                    //    _logger.LogInformation("aboba");
                    //}
                    Guid gameId = Guid.Empty;
                    var checkHaveSteamId = await Sender.Send(new GetGamesIdBySteamIdQuery(item.appid));
                    if (checkHaveSteamId is null){
                        gameId = await _steamApiCompilation.AddGame(item.appid);
                    }
                    else
                    {
                        gameId = checkHaveSteamId.GameId;
                    }
                    
                    if(gameId == Guid.Empty)
                    {
                        continue;
                    }

                    var statusGame = StatusGameProgressions.Playing;
                    if (item.rtime_last_played == 0)
                    {
                        statusGame = StatusGameProgressions.Planned;
                    }
                    if (DateTimeOffset.FromUnixTimeSeconds(item.rtime_last_played).UtcDateTime < DateTimeOffset.Now.AddMonths(-4))
                    {
                        statusGame = StatusGameProgressions.Dropped;
                    }
                    await Sender.Send(new CreateProfileHasGameCommand(profile.Id, gameId, statusGame, item.playtime_forever));
                    _logger.LogInformation($"Add game to profile {userInfo[1]} {gameId}");
                }
            }

            profile = await Sender.Send(query);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, profile.Id.ToString()) };
            ClaimsIdentity claimsIdentity = new(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            var getCache = await _cacheService.GetAsync<UserCache>(profile.Id.ToString());
            List<UserDevice> list = new();
            if (getCache is not null)
            {
                list = getCache.DeviceList;
            }

            // cookie for redis
            var userCache = new UserCache() { AvatarImage = new Uri(userInfo[0]), DeviceList = list };
            var cookie = Response.Headers["Set-Cookie"].ToString().Split('=')[1].Split(';')[0];

            if (list.Where(x => x.UserAgent == Request.Headers.UserAgent.ToString()).Count() > 0)
            {
                list.Where(x => x.UserAgent == Request.Headers.UserAgent.ToString()).First().SessionCookie = cookie;
            }
            else
            {
                userCache.DeviceList.Add(new() { UserAgent = Request.Headers.UserAgent.ToString(), SessionCookie = cookie });
            }
           
            await _cacheService.SetAsync(profile.Id.ToString(), userCache);
            _logger.LogInformation($"{userInfo[1]} is Steam login");
            return Ok(new AnswerLoginSteam() { Name = userInfo[1], Avatar = userInfo[0]});
        }

        
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            var userCache = await _cacheService.GetAsync<UserCache>(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
            var cache = userCache.DeviceList.Where(device => device.UserAgent == Request.Headers.UserAgent).FirstOrDefault();
            cache.SessionCookie = "";
            await _cacheService.SetAsync(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value, userCache);
            _logger.LogInformation($"Logged out {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value}");
            return Ok();
        }

    }
    class AnswerLoginSteam
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
