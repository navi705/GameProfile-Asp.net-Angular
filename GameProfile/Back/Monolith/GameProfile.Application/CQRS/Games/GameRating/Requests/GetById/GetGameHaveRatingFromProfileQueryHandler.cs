using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameRating.Requests.GetById
{
    public sealed class GetGameHaveRatingFromProfileQueryHandler : IRequestHandler<GetGameHaveRatingFromProfileQuery, GameHasRatingFromProfile>
    {
        private readonly IDatabaseContext _context;

        public GetGameHaveRatingFromProfileQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GameHasRatingFromProfile> Handle(GetGameHaveRatingFromProfileQuery request, CancellationToken cancellationToken)
        {
          return await _context.GameHasRatingFromProfiles.Where(x=>x.ProfileId == request.ProfileId && x.GameId == request.GameId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
