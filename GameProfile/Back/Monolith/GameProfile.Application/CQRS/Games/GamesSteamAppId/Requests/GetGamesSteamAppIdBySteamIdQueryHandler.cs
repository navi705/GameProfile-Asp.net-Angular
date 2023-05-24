using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests
{
    public class GetGamesSteamAppIdBySteamIdQueryHandler : IRequestHandler<GetGamesSteamAppIdBySteamIdQuery, GameSteamId>
    {
        private readonly IDatabaseContext _context;

        public GetGamesSteamAppIdBySteamIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public Task<GameSteamId> Handle(GetGamesSteamAppIdBySteamIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.GameSteamIds.Where(x => x.SteamAppId == request.steamId).FirstOrDefault();
            return Task.FromResult(query);    
        }
    }
}
