using GameProfile.Application.Data;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameRating.Commands.Create
{
    public sealed class CreateGameHaveRatingFromProfileCommandHandler : IRequestHandler<CreateGameHaveRatingFromProfileCommand>
    {
        private readonly IDatabaseContext _context;

        public CreateGameHaveRatingFromProfileCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateGameHaveRatingFromProfileCommand request, CancellationToken cancellationToken)
        {
            var gameHaveRatingFromProfile = new GameHasRatingFromProfile(Guid.Empty, request.ProfileId, request.GameId, request.score);
            await _context.GameHasRatingFromProfiles.AddAsync(gameHaveRatingFromProfile, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
