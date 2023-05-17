using GameProfile.Application.Data;
using GameProfile.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesBySort
{
    public sealed record class GetGamesBySortQueryHandler : IRequestHandler<GetGamesBySortQuery, List<Game>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesBySortQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<Game>> Handle(GetGamesBySortQuery request, CancellationToken cancellationToken)
        {
            if(request.sort == "titleAtoZ")
            {
                var games = _context.Games.FromSqlRaw($"select * from Games").OrderBy(x=>x.Title).ToList();
               // var games = _context.Games.OrderBy(x=>x.Title).ToList();
                return Task.FromResult(games);
            }
            if(request.sort == "titleZtoA")
            {
                var games = _context.Games.FromSqlRaw($"select * from Games").OrderByDescending(x => x.Title).ToList();
                //var games = _context.Games.OrderByDescending(x => x.Title).ToList();
                return Task.FromResult(games);
            }
            if (request.sort == "dateAscending")
            {
                //var games = _context.Games.FromSqlRaw($"select * from Games order by ReleaseDate ASC OFFSET 0 ROWS").ToList();
                var games = _context.Games.OrderBy(x => x.ReleaseDate).ToList();
                return Task.FromResult(games);
            }
            if (request.sort == "dateDescending")
            {
                //var games = _context.Games.FromSqlRaw($"select * from Games order by ReleaseDate DESC OFFSET 0 ROWS").ToList();
                var games = _context.Games.OrderByDescending(x => x.ReleaseDate).ToList();
                return Task.FromResult(games);
            }
            var games1 = _context.Games.FromSqlRaw($"select * from Games").ToList();
            //var games1 = _context.Games.ToList();
            return Task.FromResult(games1);
        }
    }
}
