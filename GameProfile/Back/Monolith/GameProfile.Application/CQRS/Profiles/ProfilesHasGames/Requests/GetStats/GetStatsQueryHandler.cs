using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats
{
    public sealed class GetStatsQueryHandler : IRequestHandler<GetStatsProfilesQuery, List<StatsDTO>>
    {
        private readonly IDatabaseContext _context;
        public GetStatsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<StatsDTO>> Handle(GetStatsProfilesQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Profiles.AsNoTracking()
            .Include(p => p.ProfileHasGames)
            .Select(p => new StatsDTO(
                 p.Id,
                 p.Name.Value,
                 p.ProfileHasGames.Sum(phg => phg.MinutesInGame + phg.MinutesInGameVerified) / 60,
                 p.ProfileHasGames.Sum(phg => phg.MinutesInGameVerified) / 60,
                 p.ProfileHasGames.Sum(phg => phg.MinutesInGame) / 60,
                 p.ProfileHasGames.Count())).ToListAsync(cancellationToken);

            result = result.OrderByDescending(x => x.HoursVerificated).ToList();

            return result;
        }
    }
}
