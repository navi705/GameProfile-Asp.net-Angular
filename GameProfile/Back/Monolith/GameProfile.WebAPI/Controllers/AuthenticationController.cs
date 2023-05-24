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
            string idUser = steamOpenIdData.openidclaimed_id.Substring(37);
            bool authIsValid = await steamApi.CheckOpenIdSteam(steamOpenIdData);
            if (authIsValid == false)
            {
                return Unauthorized();
            }
            var userInfo = await steamApi.SteamUserGetPlayerSummaries(idUser);

            var query = new GetProfileQuery(idUser);
           var profile= await Sender.Send(query);

            if (profile is null)
            {
                var query2 = new CreateProfileCommand(userInfo[1], "", idUser);
                await Sender.Send(query2);
                SteamApi steamApis = new();
                var games = steamApis.SteamOwnedGames(idUser);
                profile = await Sender.Send(query); 
                foreach (var item in games.Result.games)
                {
                    //// var game= steamApi.GetgameInfo(item.appid);
                    //  if (game is null)
                    //      continue;
                    //  profile = await Sender.Send(query);
                    //  if (game.Result is null)
                    //      continue;
                    //  if (game.Result.Name is null)
                    //      continue;

                    //var query1 = new GetGameByNameQuery(game.Result.Name);
                    //var result1 = await Sender.Send(query1);
                    //if(result1 is null)
                    //{
                    //    continue;
                    //}
                    var query3 = new GetGamesSteamAppIdBySteamIdQuery(item.appid);
                    var res = await Sender.Send(query3);
                    if(res is null)
                    {
                        continue;
                        // in future maybe
                    }
                    var statusGame = StatusGameProgressions.Playing;
                    if(item.rtime_last_played == 0)
                    {
                        statusGame = StatusGameProgressions.Planned;
                    }
                    if(DateTimeOffset.FromUnixTimeSeconds(item.rtime_last_played).UtcDateTime < DateTimeOffset.Now.AddMonths(-4))
                    {
                        statusGame = StatusGameProgressions.Dropped;
                    }
                    var query4 = new CreateProfileHasGameCommand(profile.Id,res.GameId,statusGame,item.playtime_forever);
                    await Sender.Send(query4);
                }
                   
            }
            profile = await Sender.Send(query);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, profile.Id.ToString()) };           
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            var userCache = new UserCache() { AvatarImage= new Uri(userInfo[0]),DeviceList = new()};
            var cookie = Response.Headers["Set-Cookie"].ToString().Split('=')[1].Split(';')[0];
            userCache.DeviceList.Add(new() {UserAgent= Request.Headers.UserAgent.ToString(),SessionCookie= cookie});

            await _cacheService.SetAsync(profile.Id.ToString(),userCache);
         
            var anser = new AnswerLoginSteam() { name = userInfo[1], avatar = userInfo[0] };
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
            await _cacheService.SetAsync(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value,userCache);
            return Ok();
        }

    }
    class AnswerLoginSteam
    {
        public string name { get; set; }
        public string avatar { get; set; }
    }
}
