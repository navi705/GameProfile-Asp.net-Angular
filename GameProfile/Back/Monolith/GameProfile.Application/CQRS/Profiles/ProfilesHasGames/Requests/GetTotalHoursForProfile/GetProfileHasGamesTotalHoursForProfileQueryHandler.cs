using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetTotalHoursForProfile
{
    public sealed class GetProfileHasGamesTotalHoursForProfileQueryHandler : IRequestHandler<GetProfileHasGamesTotalHoursForProfileQuery,List<int>>
    {
        private readonly IDatabaseContext _context;

        public GetProfileHasGamesTotalHoursForProfileQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<int>> Handle(GetProfileHasGamesTotalHoursForProfileQuery request, CancellationToken cancellationToken)
        {
            var totalHours = await _context.ProfileHasGames.AsNoTracking().Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGame + x.MinutesInGameVerified,cancellationToken) / 60;
            var totalHoursVerified = await _context.ProfileHasGames.AsNoTracking().Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGameVerified,cancellationToken) / 60;
            var totalHoursNotVerified = await _context.ProfileHasGames.AsNoTracking().Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGame,cancellationToken) / 60;
            return new List<int> { totalHours, totalHoursVerified, totalHoursNotVerified };
        }
    }
}
