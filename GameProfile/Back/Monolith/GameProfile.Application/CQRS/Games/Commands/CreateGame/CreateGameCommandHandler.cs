using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.Games.Commands.CreateGame
{
    public sealed class CreateGameCommandHandler : IRequestHandler<CreateGameCommand>
    {
        private readonly IDatabaseContext _context;

        public CreateGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var game = new Game(Guid.Empty,
                                request.Title,
                                request.ReleaseDate,
                                request.HeaderImage,
                                request.BackgroundImage,
                                request.NSFW,
                                request.Description,
                                request.Developers,
                                request.Publishers,
                                request.Genres,
                                request.Tags,
                                request.Screenshots,
                                request.ShopsLinkBuyGame,
                                request.Reviews,
                                request.AchievementsCount);
            _context.Games.Add(game);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
