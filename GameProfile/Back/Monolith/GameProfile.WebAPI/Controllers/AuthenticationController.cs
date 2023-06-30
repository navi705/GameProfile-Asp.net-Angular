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
using GameProfile.Application.Games.Commands.CreateGame;

namespace GameProfile.WebAPI.Controllers
{
    public sealed class AuthenticationController : ApiController
    {
        private readonly ICacheService _cacheService;
        public AuthenticationController(ISender sender, ICacheService cacheService) : base(sender)
        {
            _cacheService = cacheService;
        }

        [AllowAnonymous]
        [HttpPost("login/steam")]
        public async Task<IActionResult> LoginBySteam(SteamOpenIdData steamOpenIdData)
        {
            SteamApi steamApi = new();
            string idUser = steamOpenIdData.openidclaimed_id[37..];
            if (await steamApi.CheckOpenIdSteam(steamOpenIdData) == false)
            {
                return Unauthorized();
            }

            var games = await steamApi.SteamOwnedGames(idUser);
            if (games.games is null)
            {
                return BadRequest("Check your profile settings");
            }

            var userInfo = await steamApi.SteamUserGetPlayerSummaries(idUser);

            var query = new GetProfileQuery(idUser);
            var profile = await Sender.Send(query);
            if (profile is null)
            {
                var query2 = new CreateProfileCommand(userInfo[1], "", idUser);
                await Sender.Send(query2);
                profile = await Sender.Send(query);
                foreach (var item in games.games)
                {
                    var query3 = new GetGamesIdBySteamIdQuery(item.appid);
                    var res = await Sender.Send(query3);
                    if (res is null)
                    {
                        var game = await steamApi.GetgameInfo(item.appid);
                        if (game is null)
                        {
                            continue;
                        }
                        await Sender.Send(new CreateGameCommand(game.Name, game.ReleaseTime, game.HeaderImg, game.Nsfw, "", game.Genres, game.Publishers, game.Developers, null, null, 0));
                        var query6 = await Sender.Send(new GetGameByNameQuery(game.Name));
                        await Sender.Send(new CreateGamesSteamAppIdQuery(query6.Id, item.appid));
                        res = await Sender.Send(query3);
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
                    await Sender.Send(new CreateProfileHasGameCommand(profile.Id, res.GameId, statusGame, item.playtime_forever));
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
            

            var userCache = new UserCache() { AvatarImage = new Uri(userInfo[0]),DeviceList=list };
            var cookie = Response.Headers["Set-Cookie"].ToString().Split('=')[1].Split(';')[0];
            userCache.DeviceList.Add(new() { UserAgent = Request.Headers.UserAgent.ToString(), SessionCookie = cookie });

            await _cacheService.SetAsync(profile.Id.ToString(), userCache);

            var anser = new AnswerLoginSteam() { Name = userInfo[1], Avatar = userInfo[0] };

            return Ok(anser);
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            var userCache = await _cacheService.GetAsync<UserCache>(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
            var cache = userCache.DeviceList.Where(device => device.UserAgent == Request.Headers.UserAgent).FirstOrDefault();
            cache.SessionCookie = "";
            await _cacheService.SetAsync(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value, userCache);
            return Ok();
        }

    }
    class AnswerLoginSteam
    {
        public string Name { get; set; }
        public string Avatar { get; set; }
    }
}
