using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.UpdateGameReviews
{
    public sealed class UpdateGameReviewsCommandHandler : IRequestHandler<UpdateGameReviewsCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateGameReviewsCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateGameReviewsCommand request, CancellationToken cancellationToken)
        {
            var game =  await _context.Games.FindAsync(request.GameId,cancellationToken);
            if(game is not null)
            {
                var haveGameProfileReview = game.Reviews.Where(x => x.Site == request.Review.Site).FirstOrDefault();
                if(haveGameProfileReview is not null)
                {
                    haveGameProfileReview.Score = request.Review.Score;
                }
                else
                {
                    game.Reviews.Add(request.Review);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
