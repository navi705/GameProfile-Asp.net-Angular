using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Commands.UpdateGame
{
    public sealed class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            //await _context.Games.Where(game => game.Id == request.GameId).ExecuteDeleteAsync(cancellationToken);
            //var game = new Game(request.GameId,
            //                    request.Game.Title,
            //                    request.Game.ReleaseDate,
            //                    request.Game.HeaderImage,
            //                    request.Game.Nsfw,
            //                    request.Game.Description,
            //                    request.Game.Developers,
            //                    request.Game.Publishers,
            //                    request.Game.Genres,
            //                    request.Game.Screenshots,
            //                    request.Game.ShopsLinkBuyGame,
            //                    request.Game.AchievementsCount);
            //_context.Games.Add(game);
            //await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
