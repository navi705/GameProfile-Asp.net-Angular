using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var query = _context.Games.AsQueryable();

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
                query = query.OrderBy(x => x.ReleaseDate);
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

            int skipGame = request.Page * 50;
            query = query.Skip(skipGame).Take(50);
            var games = await query.ToListAsync(cancellationToken);

            return games;
        }
    }
}
