using GameProfile.Application.Data;
using GameProfile.Domain.Enums.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHoursVerification
{
    public sealed class GetProfileHasGamesTotalHoursVerificationQueryHandler : IRequestHandler<GetProfileHasGamesTotalHoursVerificationQuery,int>
    {
        private readonly IDatabaseContext _context;

        public GetProfileHasGamesTotalHoursVerificationQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(GetProfileHasGamesTotalHoursVerificationQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ProfileHasGames.AsNoTracking().AsQueryable();
            if (request.Filter == "1")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Playing);
            }
            if (request.Filter == "2")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Completed);
            }
            if (request.Filter == "3")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Dropped);
            }
            if (request.Filter == "4")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Planned);
            }

            return await query.Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGameVerified) / 60;
        }
    }
}
