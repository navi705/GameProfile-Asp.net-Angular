using GameProfile.Application;
using GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests;
using GameProfile.Application.CQRS.Profiles.AddSteamId;
using GameProfile.Application.CQRS.Profiles.Notification.Commands.Delete;
using GameProfile.Application.CQRS.Profiles.Notification.Requests.GetByProfileId;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.DeleteProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateValidHours;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGameBySteamId;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHours;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHoursVerification;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesWithDataByProfileId;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasOneGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileSteamIds;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetTotalHoursForProfile;
using GameProfile.Application.CQRS.Profiles.Requests.GetProfileById;
using GameProfile.Application.CQRS.Profiles.Requests.GetSteamIdBySteamId;
using GameProfile.Domain.AggregateRoots.Profile;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Enums.Profile;
using GameProfile.Infrastructure.Steam;
using GameProfile.Persistence.Caching;
using GameProfile.WebAPI.ApiCompilation;
using GameProfile.WebAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly ICacheService _cacheService;
        private readonly ISteamApi _steamApi;
        private readonly SteamApiCompilation _steamApiCompilation;
        public ProfileController(ISender sender, ICacheService cacheService, ISteamApi steamApi) : base(sender)
        {
            _cacheService = cacheService;
            _steamApi = steamApi;
            _steamApiCompilation = new(sender, steamApi);
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile(string? filter, string? sort, string? verification)
        {
            var userCache = await _cacheService.GetAsync<UserCache>(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);

            var qeury = new GetProfileHasGamesWithDataByProfileIdQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), filter, sort, verification);
            var games = await Sender.Send(qeury);

            var qeury2 = new GetProfileByIdQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            var profile = await Sender.Send(qeury2);


            int hoursForSort = 0;
            if (verification == "yes")
            {
                var query4 = new GetProfileHasGamesTotalHoursVerificationQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), filter);
                hoursForSort = await Sender.Send(query4);
            }
            else
            {
                var query3 = new GetProfileHasGamesTotalHoursQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), filter);
                hoursForSort = await Sender.Send(query3);
            }

            List<int> hoursProfile = await Sender.Send(new GetProfileHasGamesTotalHoursForProfileQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)));

            var answer = new AnswerForProfile()
            {
                NickName = profile.Name.Value.ToString(),
                Description = profile.Description.Value.ToString(),
                Avatar = userCache.AvatarImage.ToString().Replace("_medium", "_full"),
                TotalHours = hoursProfile[0],
                TotalHoursVerification = hoursProfile[1],
                TotalHoursNotVerification = hoursProfile[2],
                TotalHoursForSort = hoursForSort,
                GameList = games
            };

            return Ok(answer);
        }
        [AllowAnonymous]
        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> ProfileViewId(string? filter, string? sort, Guid profileId, string? verification)
        {
            var userCache = await _cacheService.GetAsync<UserCache>(profileId.ToString());


            var qeury = new GetProfileHasGamesWithDataByProfileIdQuery(profileId, filter, sort, verification);
            var games = await Sender.Send(qeury);

            var qeury2 = new GetProfileByIdQuery(profileId);
            var profile = await Sender.Send(qeury2);


            int hoursForSort = 0;
            if (verification == "yes")
            {
                var query4 = new GetProfileHasGamesTotalHoursVerificationQuery(profileId, filter);
                hoursForSort = await Sender.Send(query4);
            }
            else
            {
                var query3 = new GetProfileHasGamesTotalHoursQuery(profileId, filter);
                hoursForSort = await Sender.Send(query3);
            }

            List<int> hoursProfile = await Sender.Send(new GetProfileHasGamesTotalHoursForProfileQuery(profileId));

            var answer = new AnswerForProfile()
            {
                NickName = profile.Name.Value.ToString(),
                Description = profile.Description.Value.ToString(),
                Avatar = userCache.AvatarImage.ToString().Replace("_medium", "_full"),
                TotalHours = hoursProfile[0],
                TotalHoursVerification = hoursProfile[1],
                TotalHoursNotVerification = hoursProfile[2],
                TotalHoursForSort = hoursForSort,
                GameList = games
            };

            return Ok(answer);
        }


        [Authorize]
        [HttpGet("profile/avatar")]
        public async Task<IActionResult> GetProfileAvatar()
        {
            var userCache = await _cacheService.GetAsync<UserCache>(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
            if (userCache is null)
            {
                return Ok();
            }
            if (!userCache.DeviceList.Any(device => device.UserAgent == Request.Headers.UserAgent && device.SessionCookie == Request.Cookies[".Auth.Cookies"]))
            {
                return Unauthorized();
            }
            return Ok(userCache.AvatarImage);
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("profile/update/game")]
        public async Task<IActionResult> UpdateProileHasGame(Guid gameId, int hours, StatusGameProgressions statusGame)
        {
            var query1 = new GetProfileHasOneGameQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), gameId);
            var gameProfile = await Sender.Send(query1);
            if (gameProfile is null)
            {
                return BadRequest();
            }
            var query = new UpdateProfileHasGameCommand(gameId, hours, statusGame, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), gameProfile.HoursVereficated);
            await Sender.Send(query);
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("profile/delete/game")]
        public async Task<IActionResult> DeleteProileHasGame(Guid gameId)
        {
            var query = new DeleteProfileHasGameCommand(gameId, new Guid((HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)));
            await Sender.Send(query);
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("profile/add/game")]
        public async Task<IActionResult> ProfileHasGameAddGame(Guid gameId, StatusGameProgressions status, int hours)
        {
            var query1 = new GetProfileHasOneGameQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), gameId);
            var gameProfile = await Sender.Send(query1);
            if (gameProfile is not null)
            {
                return NoContent();
            }
            var query = new CreateProfileHasGameCommand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), gameId, status, hours * 60, 0);
            await Sender.Send(query);
            return Ok();
        }


        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile/get/game")]
        public async Task<IActionResult> ProfileHasGameGetGame(Guid gameId)
        {
            var query = new GetProfileHasOneGameQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), gameId);
            return Ok(await Sender.Send(query));
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile/notification")]
        public async Task<IActionResult> ProfileGetNotification()
        {
            var query = new GetProfileNotificationByIdQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            return Ok(await Sender.Send(query));
        }


        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("profile/notification")]
        public async Task<IActionResult> ProfileDeleteNotification(string notification)
        {
            var query = new DeleteProfileNotificationComand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), notification);
            await Sender.Send(query);
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("profile/update/valide-time")]
        public async Task<IActionResult> UpdateSteamTime()
        {
            var userCache = await _cacheService.GetAsync<UserCache>(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);


            if (userCache.SteamUpdateTime.CountUpdateSteam > 2)
            {
                return Forbid();
            }

            if (userCache.SteamUpdateTime.DateTime.AddHours(24) > DateTime.Now)
            {
                return Forbid();

            }

            var steamIds = await Sender.Send(new GetProfileSteamIdsQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)));
            int i = 0;
            foreach (var steamId in steamIds)
            {
                var games = await _steamApi.SteamOwnedGames(steamId);
                if (games.games is null)
                {
                    return Forbid("Check your profile settings");
                }

                foreach (var game in games.games)
                {
                    var query = new GetProfileHasGameBySteamIdQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), game.appid);
                    var gameProfile = await Sender.Send(query);

                    if (gameProfile is null)
                    {
                        var query3 = new GetGamesIdBySteamIdQuery(game.appid);
                        var gameInfo = await Sender.Send(query3);

                        var gameId = Guid.Empty;

                        if (gameInfo is null)
                        {
                            gameId = await _steamApiCompilation.AddGame(game.appid);
                        }
                        else
                        {
                            gameId = gameInfo.GameId;
                        }

                        if (gameId == Guid.Empty)
                        {
                            continue;
                        }


                        var query2 = new CreateProfileHasGameCommand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), gameId, StatusGameProgressions.Playing, 0, game.playtime_forever);
                        await Sender.Send(query2);
                        continue;
                    }

                    if (i > 0)
                    {
                        var query6 = new UpdateVerificatedMinutesProfileHasGameCommand(gameProfile.ProfileId, gameProfile.GameId, game.playtime_forever);
                        // + gameProfile.MinutesInGameVerified
                        await Sender.Send(query6);
                        continue;
                    }
                    var query1 = new UpdateVerificatedMinutesProfileHasGameCommand(gameProfile.ProfileId, gameProfile.GameId, game.playtime_forever);
                    await Sender.Send(query1);
                }
                i++;
            }

            userCache.SteamUpdateTime.CountUpdateSteam++;
            if (userCache.SteamUpdateTime.CountUpdateSteam > 2)
            {
                userCache.SteamUpdateTime.CountUpdateSteam = 0;
                userCache.SteamUpdateTime.DateTime = DateTime.Now;
            }
            await _cacheService.SetAsync(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value, userCache);

            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("profile/steam/add")]
        public async Task<IActionResult> AddSteamProfile(SteamOpenIdData steamOpenIdData)
        {
            string idUser = steamOpenIdData.openidclaimed_id[37..];
            if (await _steamApi.CheckOpenIdSteam(steamOpenIdData) == false)
            {
                return Unauthorized();
            }

            var games = await _steamApi.SteamOwnedGames(idUser);
            if (games.games is null)
            {
                return BadRequest("Check your profile settings");
            }

            var haveSteamId = await Sender.Send(new GetSteamIdBySteamIdQuery(idUser));

            if (haveSteamId)
            {
                return BadRequest("This steamId already have");
            }

            await Sender.Send(new AddProfileSteamIdCommand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), idUser));

            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile/steams")]
        public async Task<IActionResult> GetSteamProfile()
        {
            var query = new GetProfileSteamIdsQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            return Ok(await Sender.Send(query));
        }

        public class AnswerForProfile
        {
            public string NickName { get; set; }

            public string Description { get; set; }

            public string Avatar { get; set; }

            public int TotalHours { get; set; }

            public int TotalHoursVerification { get; set; }

            public int TotalHoursNotVerification { get; set; }

            public int TotalHoursForSort { get; set; }

            public List<AggregateProfileHasGame> GameList { get; set; }

        }
    }
}
