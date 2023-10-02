using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameRating.Commands.Update
{
    public sealed class UpdateGameHaveRatingFromProfileCommandHanlder : IRequestHandler<UpdateGameHaveRatingFromProfileCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateGameHaveRatingFromProfileCommandHanlder(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateGameHaveRatingFromProfileCommand request, CancellationToken cancellationToken)
        {
           var gameHaveRatingFromProfile = await _context.GameHasRatingFromProfiles.Where(x=>x.ProfileId == request.ProfileId && x.GameId == request.GameId).FirstOrDefaultAsync(cancellationToken);
            gameHaveRatingFromProfile.ReviewScore = request.score;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
