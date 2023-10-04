using GameProfile.Application;
using GameProfile.Application.CQRS.Profiles.Notification.Commands.Delete;
using GameProfile.Application.CQRS.Profiles.Notification.Requests.GetByProfileId;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.DeleteProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHours;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHoursVerification;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesWithDataByProfileId;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasOneGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetTotalHoursForProfile;
using GameProfile.Application.CQRS.Profiles.Requests.GetProfileById;
using GameProfile.Domain.AggregateRoots.Profile;
using GameProfile.Domain.Enums.Profile;
using GameProfile.Persistence.Caching;
using GameProfile.WebAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile(string? filter, string? sort, string? verification)
        {
            var userCache = await _cacheService.GetAsync<UserCache>(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);

            var qeury = new GetProfileHasGamesWithDataByProfileIdQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), filter, sort,verification);
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
            var query = new UpdateProfileHasGameCommand(gameId, hours, statusGame, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value),gameProfile.HoursVereficated);
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
            var query = new CreateProfileHasGameCommand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), gameId, status, hours * 60,0);
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
