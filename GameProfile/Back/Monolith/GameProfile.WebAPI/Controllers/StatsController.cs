using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats.GetCount;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    public class StatsController : ApiController
    {
        private readonly ILogger<StatsController> _logger;
        public StatsController(ISender sender, ILogger<StatsController> logger) : base(sender)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("stats")]
        public async Task<IActionResult> Stats()
        {
            var query = new GetStatsProfilesQuery();
            _logger.LogInformation("Someone get stats");
            return Ok(await Sender.Send(query));
        }

        [AllowAnonymous]
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var query = new GetCountProfilesQuery();
            _logger.LogInformation("Someone get count of profiles");
            return Ok(await Sender.Send(query));
        }

    }
}
