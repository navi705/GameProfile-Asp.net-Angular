using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesByPubOrDev
{
    public sealed class GetGamesByPubOrDevHandler : IRequestHandler<GetGamesByPubOrDevQuery, List<GamesDTO>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesByPubOrDevHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<GamesDTO>> Handle(GetGamesByPubOrDevQuery request, CancellationToken cancellationToken)
        {
            List<GamesDTO> games = new();
            if (request.ProfileId != Guid.Empty)
            {
                if (request.Type == "developer")
                {
                    games = await _context.Games.AsNoTracking().Where(g => g.Developers.Any(d => d.GameString == request.Who))
                        .GroupJoin(_context.ProfileHasGames.Where(p => p.ProfileId == request.ProfileId), game => game.Id,
                    profileGame => profileGame.GameId,
                    (game, profileGames) => new { Game = game, ProfileGames = profileGames })
                    .SelectMany(x => x.ProfileGames.DefaultIfEmpty(),
                    (x, profileGame) => new GamesDTO(
                        x.Game.Id,
                        x.Game.Title,
                        x.Game.ReleaseDate,
                        x.Game.HeaderImage,
                        x.Game.Developers.Select(d => d.GameString).ToList(),
                        x.Game.Publishers.Select(d => d.GameString).ToList(),
                        x.Game.Genres.Select(d => d.GameString).ToList(),
                        x.Game.Reviews,
                        x.Game.ProfileHasGames.Select(g=>g.StatusGame).FirstOrDefault()
                        )).ToListAsync(cancellationToken);

                }
                if (request.Type == "publisher")
                {
                    games = await _context.Games.AsNoTracking().Where(g => g.Publishers.Any(d => d.GameString == request.Who))
                       .GroupJoin(_context.ProfileHasGames.Where(p => p.ProfileId == request.ProfileId), game => game.Id,
                    profileGame => profileGame.GameId,
                    (game, profileGames) => new { Game = game, ProfileGames = profileGames })
                    .SelectMany(x => x.ProfileGames.DefaultIfEmpty(),
                    (x, profileGame) => new GamesDTO(
                        x.Game.Id,
                        x.Game.Title,
                        x.Game.ReleaseDate,
                        x.Game.HeaderImage,
                        x.Game.Developers.Select(d => d.GameString).ToList(),
                        x.Game.Publishers.Select(d => d.GameString).ToList(),
                        x.Game.Genres.Select(d => d.GameString).ToList(),
                        x.Game.Reviews,
                        x.Game.ProfileHasGames.Select(g => g.StatusGame).FirstOrDefault()
                        )).ToListAsync(cancellationToken);
                }
            }
            else
            {
                if (request.Type == "developer")
                {
                    games = await _context.Games.AsNoTracking().Where(g => g.Developers.Any(d => d.GameString == request.Who)).Select(x => new GamesDTO(x.Id,
                        x.Title,
                        x.ReleaseDate,
                        x.HeaderImage,
                        x.Developers.Select(d => d.GameString).ToList(),
                        x.Publishers.Select(d => d.GameString).ToList(),
                        x.Genres.Select(d => d.GameString).ToList(),
                        x.Reviews,
                        null)).ToListAsync(cancellationToken);

                }
                if (request.Type == "publisher")
                {
                    games = await _context.Games.AsNoTracking().Where(g => g.Publishers.Any(d => d.GameString == request.Who)).Select(x => new GamesDTO(
                        x.Id,
                        x.Title,
                        x.ReleaseDate,
                        x.HeaderImage,
                        x.Developers.Select(d => d.GameString).ToList(),
                        x.Publishers.Select(d => d.GameString).ToList(),
                        x.Genres.Select(d => d.GameString).ToList(),
                        x.Reviews,
                        null)).ToListAsync(cancellationToken);
                }
            }
            return games;
        }
    }
}
