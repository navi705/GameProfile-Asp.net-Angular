using GameProfile.Application.Data;
using GameProfile.Domain.ValueObjects.Game;
using MediatR;
using Microsoft.EntityFrameworkCore;
using GameProfile.Domain.Entities.ProfileEntites;

namespace GameProfile.Application.CQRS.Games.GameRating.Commands.GameRatingComp
{
    public sealed class GameRatingCompCommandHanlder : IRequestHandler<GameRatingCompCommand>
    {
        private readonly IDatabaseContext _context;

        public GameRatingCompCommandHanlder(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(GameRatingCompCommand request, CancellationToken cancellationToken)
        {
            var gameRating = await _context.GameHasRatingFromProfiles.Where(x => x.ProfileId == request.ProfileId && x.GameId == request.GameId).FirstOrDefaultAsync(cancellationToken);

            if (gameRating is null)
            {
                if (request.Score == 0)
                {
                    return;
                }
                var gameHaveRatingFromProfile = new GameHasRatingFromProfile(Guid.Empty, request.ProfileId, request.GameId, request.Score);
                await _context.GameHasRatingFromProfiles.AddAsync(gameHaveRatingFromProfile, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                if (request.Score == 0)
                {
                    await _context.GameHasRatingFromProfiles.Where(x => x.ProfileId == request.ProfileId && x.GameId == request.GameId).ExecuteDeleteAsync(cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);

                    var gameHasRating1 = await _context.GameHasRatingFromProfiles.Where(x => x.GameId == request.GameId).ToListAsync(cancellationToken);
                    decimal avarageRating1 = 0;
                    if (gameHasRating1.Count > 0)
                    {
                        avarageRating1 = gameHasRating1.Sum(x => x.ReviewScore) / gameHasRating1.Count;
                    }
                    Review review1 = new(Domain.Enums.Game.SiteReviews.GameProfile, avarageRating1);

                    var game = await _context.Games.FindAsync(request.GameId, cancellationToken);
                    if (game is not null)
                    {
                        var haveGameProfileReview = game.Reviews.Where(x => x.Site == review1.Site).FirstOrDefault();
                        if (haveGameProfileReview is not null)
                        {
                            haveGameProfileReview.Score = review1.Score;
                        }
                        else
                        {
                            game.Reviews.Add(review1);
                        }
                    }
                    await _context.SaveChangesAsync(cancellationToken);

                    return;
                }

                var gameHaveRatingFromProfile = await _context.GameHasRatingFromProfiles.Where(x => x.ProfileId == request.ProfileId && x.GameId == request.GameId).FirstOrDefaultAsync(cancellationToken);
                gameHaveRatingFromProfile.ChangeReview(request.Score);
                await _context.SaveChangesAsync(cancellationToken);

            }

            var gameHasRating = await _context.GameHasRatingFromProfiles.Where(x => x.GameId == request.GameId).ToListAsync(cancellationToken);

            decimal avarageRating = gameHasRating.Sum(x => (decimal)x.ReviewScore) / gameHasRating.Count;

            Review review = new(Domain.Enums.Game.SiteReviews.GameProfile, avarageRating);

            var game1 = await _context.Games.FindAsync(request.GameId, cancellationToken);
            if (game1 is not null)
            {
                var haveGameProfileReview = game1.Reviews.Where(x => x.Site == review.Site).FirstOrDefault();
                if (haveGameProfileReview is not null)
                {
                    haveGameProfileReview.Score = review.Score;
                }
                else
                {
                    game1.Reviews.Add(review);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
