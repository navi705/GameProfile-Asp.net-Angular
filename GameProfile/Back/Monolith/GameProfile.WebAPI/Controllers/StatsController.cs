using GameProfile.Application;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats.GetCount;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    public class StatsController : ApiController
    {
        private readonly ICacheService _cacheService;
        public StatsController(ISender sender, ICacheService cacheService) : base(sender)
        {
            _cacheService = cacheService;
        }
        [HttpGet("stats")]
        public async Task<IActionResult> Stats()
        {
            var query = new GetStatsProfilesQuery();
            return Ok(await Sender.Send(query));
        }
        [HttpGet("count")]
        public async Task<IActionResult> Count()
        {
            var query = new GetCountProfilesQuery();
            return Ok(await Sender.Send(query));
        }
    }
}
