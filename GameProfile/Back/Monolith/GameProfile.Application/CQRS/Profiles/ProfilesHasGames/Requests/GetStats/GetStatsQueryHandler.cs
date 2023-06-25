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

        public Task<List<AggregateProfileStats>> Handle(GetStatsProfilesQuery request, CancellationToken cancellationToken)
        {
            var result = _context.Profiles
            .Include(p => p.ProfileHasGames)
            .ToList()
            .Select(p => new AggregateProfileStats(
                 p.Id,
                 p.Name,
                 p.ProfileHasGames.Sum(phg => phg.MinutesInGame) / 60,
                 p.ProfileHasGames.Count()))
            .OrderByDescending(x=> x.TotalHours).ToList();

            return Task.FromResult(result);
        }
    }
}
