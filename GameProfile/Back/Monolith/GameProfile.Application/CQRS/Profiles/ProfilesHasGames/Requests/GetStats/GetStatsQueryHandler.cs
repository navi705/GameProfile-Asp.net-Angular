using GameProfile.Application.Data;
using GameProfile.Domain.AggregateRoots.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats
{
    public sealed class GetStatsQueryHandler : IRequestHandler<GetStatsProfilesQuery, List<AggregateProfileStats>>
    {
        private readonly IDatabaseContext _context;
        public GetStatsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<AggregateProfileStats>> Handle(GetStatsProfilesQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Profiles.AsNoTracking()
            .Include(p => p.ProfileHasGames)
            .Select(p => new AggregateProfileStats(
                 p.Id,
                 p.Name,
                  p.ProfileHasGames.Sum(phg => phg.MinutesInGame + phg.MinutesInGameVerified) / 60,
                 p.ProfileHasGames.Sum(phg => phg.MinutesInGame) / 60,
                 p.ProfileHasGames.Sum(phg => phg.MinutesInGameVerified) / 60,
                 p.ProfileHasGames.Count())).ToListAsync(cancellationToken);

            result = result.OrderByDescending(x => x.HoursVerificated).ToList();

            return result;
        }
    }
}
