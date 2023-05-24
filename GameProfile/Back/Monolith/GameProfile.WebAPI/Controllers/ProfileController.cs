using GameProfile.Application;
using GameProfile.Application.CQRS.Games.Commands.Requests;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGameByProfileId;
using GameProfile.Application.CQRS.Profiles.Requests.GetProfileById;
using GameProfile.Domain.Enums.Profile;
using GameProfile.Infrastructure.Steam;
using GameProfile.Persistence.Caching;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    public class ProfileController : ApiController 
    {
        private readonly ICacheService _cacheService;
        public ProfileController(ISender sender, ICacheService cacheService) : base(sender)
        {
            _cacheService = cacheService;
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile()
        {
            var userCache = await _cacheService.GetAsync<UserCache>(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
            if (!userCache.DeviceList.Any(device => device.UserAgent == Request.Headers.UserAgent && device.SessionCookie == Request.Cookies[".Auth.Cookies"])) 
            {
                return Unauthorized();
            }
            // SteamApi steamApi = new();
            //var query = new GetProfileByIdQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            //var profile = await Sender.Send(query);
            //var games = steamApi.SteamOwnedGames(profile.SteamIds.First().StringFor);
            //var games = steamApi.SteamOwnedGames("76561198085715972");
            //var aboba = games.Result.games.Select(game => game.appid);
            //var str = string.Join(",", aboba);
            var query = new GetProfileHasGameByProfileIdQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            var profileHasGames = await Sender.Send(query);
            var answer = new List<GameForProfile>();
            foreach(var item in profileHasGames)
            {
                var game = await Sender.Send(new GetGameByIdQuery(item.GameId));
                answer.Add(new GameForProfile() {Id = game.Id, HeaderImage = game.HeaderImage, Title = game.Title, Hours = item.MinutesInGame/60, StatusGame = item.StatusGame});
            }
            
            return Ok(answer);
        }

        // delete in future 
        public class GameForProfile
        {
            public Guid Id { get; set; }

            public string Title { get; set; }

            public Uri HeaderImage { get; set; }

            public int Hours { get; set; }

            public StatusGameProgressions StatusGame { get; set; }
        } 
    }
}
