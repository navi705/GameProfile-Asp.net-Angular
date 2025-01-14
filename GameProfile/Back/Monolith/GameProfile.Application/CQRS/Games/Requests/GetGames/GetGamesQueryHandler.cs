﻿using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGames
{
    public sealed class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, List<GamesDTO>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<GamesDTO>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Games.AsNoTracking().AsQueryable();

            if (request.Sort == "titleAtoZ")
            {
                query = query.OrderBy(x => x.Title);               
            }
            else if (request.Sort == "titleZtoA")
            {
                query = query.OrderByDescending(x => x.Title);
            }
            else if (request.Sort == "dateAscending")
            {
                //query = query.OrderBy(x => x.ReleaseDate);
                query = query.OrderBy(x => x.ReleaseDate == DateTime.MinValue ? 1 : 0).ThenBy(x => x.ReleaseDate);
            }
            else if (request.Sort == "dateDescending")
            {
                query = query.OrderByDescending(x => x.ReleaseDate);
            }

            if (request.ReleaseDateOf != DateTime.MinValue && request.ReleaseDateTo != DateTime.MinValue)
            {
                query = query.Where(x => x.ReleaseDate >= request.ReleaseDateOf && x.ReleaseDate <= request.ReleaseDateTo);
            }
            else if (request.ReleaseDateOf == DateTime.MinValue && request.ReleaseDateTo != DateTime.MinValue)
            {
                query = query.Where(x => x.ReleaseDate <= request.ReleaseDateTo);
            }
            else if (request.ReleaseDateOf != DateTime.MinValue && request.ReleaseDateTo == DateTime.MinValue)
            {
                query = query.Where(x => x.ReleaseDate >= request.ReleaseDateOf);
            }

            if (request.Nsfw == "yes")
            {
                query = query.Where(x => x.Nsfw == true);
            }
            else if (request.Nsfw == "no")
            {
                query = query.Where(x => x.Nsfw == false);
            }

            if (request.GenresExcluding is not null && request.GenresExcluding.Count > 0)
            {
                query = query.Where(g => g.Genres.Count(gg => request.GenresExcluding.Contains(gg.GameString)) == 0);
            }
            if (request.Genres is not null && request.Genres.Count > 0)
            {
                query = query.Where(g => g.Genres.Count(gg => request.Genres.Contains(gg.GameString)) == request.Genres.Count());
            }

            if (request.TagsExcluding is not null && request.TagsExcluding.Count > 0)
            {
                query = query.Where(g => g.Tags.Count(gg => request.TagsExcluding.Contains(gg.GameString)) == 0);
            }
            if (request.Tags is not null && request.Tags.Count > 0)
            {
                query = query.Where(g => g.Tags.Count(gg => request.Tags.Contains(gg.GameString)) == request.Tags.Count());
            }

            if (request.RateOf is not null && request.RateTo is not null)
            {
                query = query.Where(x => x.Reviews.Average(x => x.Score) >= request.RateOf && x.Reviews.Average(x => x.Score) <= request.RateTo);
            }
            else if (request.RateOf is not null && request.RateTo is null)
            {
                query = query.Where(x => x.Reviews.Average(x => x.Score) >= request.RateOf);
            }
            else if (request.RateOf is null && request.RateTo is not null)
            {
                query = query.Where(x => x.Reviews.Average(x => x.Score) <= request.RateTo);
            }

            if (request.StatusGame is not null && request.StatusGame.Count > 0)
            {
                query = query.Include(f => f.ProfileHasGames).ThenInclude(f => f.Profile)
                    .Where(g => g.ProfileHasGames.Any(phg => phg.ProfileId == request.ProfileId && request.StatusGame.Contains(phg.StatusGame)));

            }
            if (request.StatusGameExcluding is not null && request.StatusGameExcluding.Count > 0)
            {
                query = query.Include(f => f.ProfileHasGames)
                .ThenInclude(phg => phg.Profile)
                .Where(g => g.ProfileHasGames.All(phg => phg.ProfileId != request.ProfileId || !request.StatusGameExcluding.Contains(phg.StatusGame)));
            }

            int skipGame = request.Page * 50;
            query = query.Skip(skipGame).Take(50);

            List<GamesDTO>? games = new();

            if (request.ProfileId != Guid.Empty)
            {
                // TODO: CHECK
                //games = await query.GroupJoin(_context.ProfileHasGames.Where(p => p.ProfileId == request.ProfileId),game => game.Id,
                //    profileGame => profileGame.GameId,
                //    (game, profileGames) => new { Game = game, ProfileGames = profileGames })
                //    .SelectMany(x => x.ProfileGames.DefaultIfEmpty(),
                //    (x, profileGame) => new Game(x.Game.Id, x.Game.Title, x.Game.ReleaseDate, x.Game.HeaderImage, null, x.Game.Nsfw, null, x.Game.Developers, x.Game.Publishers, x.Game.Genres, null, null, null, x.Game.Reviews, 0) {ProfileHasGames = x.Game.ProfileHasGames.Select(g=>new Domain.Entities.ProfileEntites.ProfileHasGames(Guid.Empty,Guid.Empty,Guid.Empty,g.StatusGame,-1,-1)).ToList() })
                //    .ToListAsync(cancellationToken);
                games = await query.GroupJoin(_context.ProfileHasGames.Where(p => p.ProfileId == request.ProfileId), game => game.Id,
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
            else
            {
                games = await query.Select(x => new GamesDTO(
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
             return games;
        }
    }
}
