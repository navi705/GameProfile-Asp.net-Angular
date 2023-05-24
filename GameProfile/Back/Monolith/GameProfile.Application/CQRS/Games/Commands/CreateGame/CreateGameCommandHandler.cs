using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.Games.Commands.CreateGame
{
    internal sealed class CreateGameCommandHandler : IRequestHandler<CreateGameCommand>
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
                                request.NSFW,
                                request.Description,
                                request.Developers,
                                request.Publishers,
                                request.Genres,                               
                                request.Screenshots,
                                request.ShopsLinkBuyGame,
                                request.AchievementsCount);
            _context.Games.Add(game);
            var nsfw = request.NSFW  ? 1 : 0;
           //var asd = await _context.ExecuteSqlInterpolatedAsync($"INSERT INTO Games values(NEWID(),{request.Title},'',{request.HeaderImage},{nsfw},{request.Description},{request.AchievementsCount})", cancellationToken);   
            await _context.SaveChangesAsync(cancellationToken);         
        }
    }
}
