using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasOneGame
{
    public class GetProfileHasOneGameQueryHandler : IRequestHandler<GetProfileHasOneGameQuery, ProfileGamesDTO>
    {
        private readonly IDatabaseContext _context;
        public GetProfileHasOneGameQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<ProfileGamesDTO> Handle(GetProfileHasOneGameQuery request, CancellationToken cancellationToken)
        {
            var query = await _context.ProfileHasGames.AsNoTracking().Where(x=>x.GameId == request.Gameid)
                .Join(_context.Games, phg => phg.GameId, g => g.Id, (phg, g) => new ProfileGamesDTO
        (
            g.Id,
            g.Title,
            g.HeaderImage,
            phg.MinutesInGame / 60,
            phg.StatusGame,
            phg.MinutesInGameVerified / 60
        )).FirstOrDefaultAsync();
            
            return query;
        }
    }
}
