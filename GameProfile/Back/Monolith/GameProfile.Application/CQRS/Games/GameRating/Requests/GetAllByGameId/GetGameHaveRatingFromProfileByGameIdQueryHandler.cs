using GameProfile.Application.Data;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameRating.Requests.GetAllByGameId
{
    public sealed class GetGameHaveRatingFromProfileByGameIdQueryHandler : IRequestHandler<GetGameHaveRatingFromProfileByGameIdQuery,List<GameHasRatingFromProfile>>
    {
        private readonly IDatabaseContext _context;

        public GetGameHaveRatingFromProfileByGameIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<GameHasRatingFromProfile>> Handle(GetGameHaveRatingFromProfileByGameIdQuery request, CancellationToken cancellationToken)
        {
            var gameHasRating = await _context.GameHasRatingFromProfiles.Where(x => x.GameId == request.GameId).ToListAsync(cancellationToken);
            return gameHasRating;
        }
    }
}
