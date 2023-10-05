using GameProfile.Application.Data;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGameBySteamId
{
    public sealed class GetProfileHasGameBySteamIdQueryHandler : IRequestHandler<GetProfileHasGameBySteamIdQuery, ProfileHasGames>
    {
        private readonly IDatabaseContext _context;

        public GetProfileHasGameBySteamIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<ProfileHasGames> Handle(GetProfileHasGameBySteamIdQuery request, CancellationToken cancellationToken)
        {
            //return await _context.ProfileHasGames.AsNoTracking().Join(_context.GameSteamIds, a => a.GameId, g => g.GameId, (a, g)=> new ProfileHasGames(a.Id,a.ProfileId,g.GameId,a.StatusGame,a.MinutesInGame,a.MinutesInGameVerified)
            //    ).Where(x => x.ProfileId == request.Profi)
            //    .FirstOrDefaultAsync(cancellationToken);

            var profileGame = await _context.ProfileHasGames
        .Join(_context.GameSteamIds,
              phg => phg.GameId,
              gsi => gsi.GameId,
              (phg, gsi) => new { ProfileHasGame = phg, GameSteamId = gsi })
        .Where(joined => joined.ProfileHasGame.ProfileId == request.ProfileId && joined.GameSteamId.SteamAppId == request.SteamId)
        .Select(joined => joined.ProfileHasGame)
        .FirstOrDefaultAsync(cancellationToken);

            return profileGame;

        }
    }
}
