﻿using GameProfile.Application;
using GameProfile.Application.CQRS.Profiles.AddSteamId;
using GameProfile.Application.CQRS.Profiles.Notification.Commands.Delete;
using GameProfile.Application.CQRS.Profiles.Notification.Requests.GetByProfileId;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.DeleteProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateProfileHasGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasOneGame;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileSteamIds;
using GameProfile.Application.CQRS.Profiles.Ranks.Requests;
using GameProfile.Application.CQRS.Profiles.Requests.GetComp;
using GameProfile.Application.CQRS.Profiles.Requests.GetSteamIdBySteamId;
using GameProfile.Domain.Enums.Profile;
using GameProfile.Infrastructure.Steam;
using GameProfile.Persistence.Caching;
using GameProfile.WebAPI.ApiCompilation;
using GameProfile.WebAPI.ApiCompilation.Controllers;
using GameProfile.WebAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    public class ProfileController : ApiController
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly ProfileComplitation _profileComplitation;
        private readonly ICacheService _cacheService;
        private readonly ISteamApi _steamApi;
       
        public ProfileController(
            ISender sender,
            ICacheService cacheService,
            ISteamApi steamApi,
            ILogger<ProfileController> logger) : base(sender)
        {
            _cacheService = cacheService;
            _steamApi = steamApi;
            _logger = logger;
            _profileComplitation = new(sender, cacheService, steamApi);
            
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile")]
        public async Task<IActionResult> Profile(string? filter, string? sort, string? verification)
        {
            var userCache = await _cacheService.GetAsync<UserCache>(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);

            var answer = await Sender.Send(new GetProfileCompQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value),
                filter,sort,verification));

            answer.Avatar = userCache.AvatarImage.ToString().Replace("_medium", "_full");
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} get own profile");
            return Ok(answer);
        }

        [AllowAnonymous]
        [HttpGet("profile/{profileId}")]
        public async Task<IActionResult> ProfileViewId(string? filter, string? sort, Guid profileId, string? verification)
        {
            var userCache = await _cacheService.GetAsync<UserCache>(profileId.ToString());

            var answer = await Sender.Send(new GetProfileCompQuery(profileId,
                filter, sort, verification));

            answer.Avatar = userCache.AvatarImage.ToString().Replace("_medium", "_full");
            _logger.LogInformation($"Someone get profile {profileId}");
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
            _logger.LogInformation($"User get avatar {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value}");
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
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} update profile have game");
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("profile/delete/game")]
        public async Task<IActionResult> DeleteProileHasGame(Guid gameId)
        {
            var query = new DeleteProfileHasGameCommand(gameId, new Guid((HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)));
            await Sender.Send(query);
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} delete profile have game");
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
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} add profile have game");
            return Ok();
        }


        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile/get/game")]
        public async Task<IActionResult> ProfileHasGameGetGame(Guid gameId)
        {
            var query = new GetProfileHasOneGameQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), gameId);
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} get profile have game");
            return Ok(await Sender.Send(query));
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile/notification")]
        public async Task<IActionResult> ProfileGetNotification()
        {
            var query = new GetProfileNotificationByIdQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} get notify");
            return Ok(await Sender.Send(query));
        }


        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("profile/notification")]
        public async Task<IActionResult> ProfileDeleteNotification(string notification)
        {
            var query = new DeleteProfileNotificationComand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), notification);
            await Sender.Send(query);
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} delete notify");
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("profile/update/valide-time")]
        public async Task<IActionResult> UpdateSteamTime()
        {
           var result = await _profileComplitation.UpdateSteamGames(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            if (!string.IsNullOrWhiteSpace(result))
            {
                return Forbid(result);
            }
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} update hours");
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
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} add steam profile");
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("profile/steams")]
        public async Task<IActionResult> GetSteamProfile()
        {
            var query = new GetProfileSteamIdsQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} get steam profiles");
            return Ok(await Sender.Send(query));
        }

        [AllowAnonymous]
        [HttpGet("profile/ranks")]
        public async Task<IActionResult> GetRanks(Guid profileId)
        {
            var query = new GetRanksQuery(profileId);
            var answer = await Sender.Send(query);
            return Ok(answer);
        }    

    }
}
