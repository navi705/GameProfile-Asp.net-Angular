using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests
{
    public class GetGamesIdBySteamIdQueryHandler : IRequestHandler<GetGamesIdBySteamIdQuery, GameSteamId?>
    {
        private readonly IDatabaseContext _context;

        public GetGamesIdBySteamIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<GameSteamId?> Handle(GetGamesIdBySteamIdQuery request, CancellationToken cancellationToken)
        {
            //var query = _context.GameSteamIds.Where(x => x.SteamAppId == request.SteamId).FirstOrDefault();
            //return Task.FromResult(query);

            var query = await _context.GameSteamIds.AsNoTracking().FirstOrDefaultAsync(x => x.SteamAppId == request.SteamId, cancellationToken);
            return query;
        }
    }
}
