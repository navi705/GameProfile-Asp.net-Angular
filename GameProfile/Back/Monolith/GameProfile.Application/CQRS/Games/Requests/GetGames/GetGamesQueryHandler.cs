using GameProfile.Application.Data;
using GameProfile.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace GameProfile.Application.CQRS.Games.Commands.Requests.GetGames
{
    public sealed class GetGamesQueryHandler : IRequestHandler<GetGamesQuery,List<Game>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<Game>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var games =  _context.Games.ToList();

            if (request.releaseDateOf != DateTime.MinValue  && request.releaseDateTo != DateTime.MinValue)
            {
                games = games.Where(x => x.ReleaseDate >= request.releaseDateOf && x.ReleaseDate <= request.releaseDateTo).ToList();
            }
            if (request.releaseDateOf == DateTime.MinValue && request.releaseDateTo != DateTime.MinValue)
            {
                games = games.Where(x =>  x.ReleaseDate <= request.releaseDateTo).ToList();
            }
            if (request.releaseDateOf != DateTime.MinValue && request.releaseDateTo == DateTime.MinValue)
            {
                games = games.Where(x => x.ReleaseDate >= request.releaseDateOf).ToList();
            }



            if (request.sort == "titleAtoZ")
            {
                games = games.OrderBy(x => x.Title).ToList();
                return Task.FromResult(games);
            }
            if (request.sort == "titleZtoA")
            {
                games = games.OrderBy(x => x.Title).OrderByDescending(x => x.Title).ToList();
                return Task.FromResult(games);
            }
            if (request.sort == "dateAscending")
            {
                games = games.OrderBy(x => x.ReleaseDate).ToList();
                return Task.FromResult(games);
            }
            if (request.sort == "dateDescending")
            {
                games = games.OrderByDescending(x => x.ReleaseDate).ToList();
                return Task.FromResult(games);
            }

            return Task.FromResult(games);
        }
    }
}
