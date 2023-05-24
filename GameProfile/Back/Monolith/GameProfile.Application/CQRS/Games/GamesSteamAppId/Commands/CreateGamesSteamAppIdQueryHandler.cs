using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GamesSteamAppId.Commands
{
    public sealed class CreateGamesSteamAppIdQueryHandler : IRequestHandler<CreateGamesSteamAppIdQuery>
    {
        private readonly IDatabaseContext _context;
        public CreateGamesSteamAppIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateGamesSteamAppIdQuery request, CancellationToken cancellationToken)
        {
            var gameSteamId = new GameSteamId(Guid.Empty, request.gameId, request.steamAppId);

            _context.GameSteamIds.Add(gameSteamId);
            await _context.SaveChangesAsync(cancellationToken); 
        }
    }
}
