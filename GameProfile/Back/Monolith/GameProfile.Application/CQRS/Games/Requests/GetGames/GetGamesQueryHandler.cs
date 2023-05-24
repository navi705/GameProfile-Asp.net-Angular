using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameProfile.Application.CQRS.Games.Commands.Requests.GetGames
{
    public sealed class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, List<Game>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            //int skipGame = request.page * 50;
            //List<Game> games = new();

            //if (request.sort == "titleAtoZ")
            //{
            //    games = _context.Games.OrderBy(x => x.Title).Skip(skipGame).Take(50).ToList();
            //}
            //if (request.sort == "titleZtoA")
            //{
            //    games = _context.Games.OrderByDescending(x => x.Title).Skip(skipGame).Take(50).ToList();
            //}
            //if (request.sort == "dateAscending")
            //{
            //    games = _context.Games.OrderBy(x => x.ReleaseDate).Skip(skipGame).Take(50).ToList();
            //}
            //if (request.sort == "dateDescending")
            //{
            //    games = _context.Games.OrderByDescending(x => x.ReleaseDate).Skip(skipGame).Take(50).ToList();
            //}

            //if (request.releaseDateOf != DateTime.MinValue && request.releaseDateTo != DateTime.MinValue)
            //{
            //    games = games.Where(x => x.ReleaseDate >= request.releaseDateOf && x.ReleaseDate <= request.releaseDateTo).ToList();
            //}
            //if (request.releaseDateOf == DateTime.MinValue && request.releaseDateTo != DateTime.MinValue)
            //{
            //    games = games.Where(x => x.ReleaseDate <= request.releaseDateTo).ToList();
            //}
            //if (request.releaseDateOf != DateTime.MinValue && request.releaseDateTo == DateTime.MinValue)
            //{
            //    games = games.Where(x => x.ReleaseDate >= request.releaseDateOf).ToList();
            //}
            //if (request.nsfw == "yes")
            //{
            //    games = games.Where(x => x.Nsfw == true).ToList();
            //}
            //if (request.nsfw == "no")
            //{
            //    games = games.Where(x => x.Nsfw == false).ToList();
            //}
            //if (request.genresExcluding is not null && request.genresExcluding.Count > 0)
            //{
            //    games = games.Where(g => !g.Genres.Any(gg => request.genresExcluding.Contains(gg.GameString))).ToList();
            //}
            //if (request.genres is not null && request.genres.Count > 0)
            //{
            //    games = games.Where(g => g.Genres.Any(gg => request.genres.Contains(gg.GameString))).ToList();
            //}


            //return Task.FromResult(games);
            //var games = _context.Games.FromSqlRaw($"select * from Games").OrderBy(x => x.Title).ToList();
            //var games = _context.Games.FromSqlRaw($"select * from Games").OrderByDescending(x => x.Title).ToList();
            //var games = _context.Games.FromSqlRaw($"select * from Games order by ReleaseDate ASC OFFSET 0 ROWS").ToList();
            //var games = _context.Games.FromSqlRaw($"select * from Games order by ReleaseDate DESC OFFSET 0 ROWS").ToList();
            var query = _context.Games.AsQueryable();

            if (request.sort == "titleAtoZ")
            {
                query = query.OrderBy(x => x.Title);
            }
            else if (request.sort == "titleZtoA")
            {
                query = query.OrderByDescending(x => x.Title);
            }
            else if (request.sort == "dateAscending")
            {
                query = query.OrderBy(x => x.ReleaseDate);
            }
            else if (request.sort == "dateDescending")
            {
                query = query.OrderByDescending(x => x.ReleaseDate);
            }

            if (request.releaseDateOf != DateTime.MinValue && request.releaseDateTo != DateTime.MinValue)
            {
                query = query.Where(x => x.ReleaseDate >= request.releaseDateOf && x.ReleaseDate <= request.releaseDateTo);
            }
            else if (request.releaseDateOf == DateTime.MinValue && request.releaseDateTo != DateTime.MinValue)
            {
                query = query.Where(x => x.ReleaseDate <= request.releaseDateTo);
            }
            else if (request.releaseDateOf != DateTime.MinValue && request.releaseDateTo == DateTime.MinValue)
            {
                query = query.Where(x => x.ReleaseDate >= request.releaseDateOf);
            }

            if (request.nsfw == "yes")
            {
                query = query.Where(x => x.Nsfw == true);
            }
            else if (request.nsfw == "no")
            {
                query = query.Where(x => x.Nsfw == false);
            }

            if (request.genresExcluding is not null && request.genresExcluding.Count > 0)
            {
                query = query.Where(g => !g.Genres.Any(gg => request.genresExcluding.Contains(gg.GameString)));
            }
            if (request.genres is not null && request.genres.Count > 0)
            {
                query = query.Where(g => g.Genres.Any(gg => request.genres.Contains(gg.GameString)));
            }

            int skipGame = request.page * 50;
            query = query.Skip(skipGame).Take(50);

            var games = await query.ToListAsync(cancellationToken);

            return games;
        }
    }
}
