using GameProfile.Application.Data;
using GameProfile.Domain.AggregateRoots.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats
{
    public sealed class GetStatsQueryHandler : IRequestHandler<GetStatsProfilesQuery, List<AggregateProfileStats>>
    {
        private readonly IDatabaseContext _context;
        public GetStatsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<AggregateProfileStats>> Handle(GetStatsProfilesQuery request, CancellationToken cancellationToken)
        {
            //       var aggregateProfileStats = _context.Profiles
            //.Join(
            //    _context.ProfileHasGames,
            //    p => p.Id,
            //    phg => phg.ProfileId,
            //    (p, phg) => new { Profile = p, ProfileHasGames = phg }
            //).AsEnumerable()
            //.GroupBy(
            //    x => new { x.Profile.Id, x.Profile.Name },
            //    (key, group) => new AggregateProfileStats(
            //        key.Id,
            //        key.Name,
            //        group.Sum(phg => phg.ProfileHasGames.MinutesInGame) / 60,
            //        group.Count()
            //    )
            //).OrderByDescending(x => x.TotalHours)
            //.ToList();

            var result = _context.Profiles
    .Include(p => p.ProfileHasGames)
    .ToList()
    .Select(p => new AggregateProfileStats(
        p.Id,
        p.Name,
        p.ProfileHasGames.Sum(phg => phg.MinutesInGame) / 60,
        p.ProfileHasGames.Count()
    ))
    .ToList();

            return Task.FromResult(result);
        }
    }
}
