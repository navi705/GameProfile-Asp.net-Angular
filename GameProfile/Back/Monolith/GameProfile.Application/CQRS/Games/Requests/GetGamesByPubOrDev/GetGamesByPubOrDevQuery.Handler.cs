using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesByPubOrDev
{
    public sealed class GetGamesByPubOrDevHandler : IRequestHandler<GetGamesByPubOrDevQuery, List<Game>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesByPubOrDevHandler(IDatabaseContext context)
        {
            _context = context;
        }
        
        public async Task<List<Game>> Handle(GetGamesByPubOrDevQuery request, CancellationToken cancellationToken)
        {
            List<Game> games = new();
            if (request.Type == "developer")
            {
                games = await _context.Games.AsNoTracking().Where(g => g.Developers.Any(d => d.GameString == request.Who)).Select(x => new Game(x.Id, x.Title, x.ReleaseDate, x.HeaderImage, null, x.Nsfw, null, x.Developers, x.Publishers, x.Genres, null, null, null, x.Reviews, 0)).ToListAsync(cancellationToken);

            }
            if (request.Type == "publisher")
            {
                 games = await _context.Games.AsNoTracking().Where(g => g.Publishers.Any(d => d.GameString == request.Who)).Select(x => new Game(x.Id, x.Title, x.ReleaseDate, x.HeaderImage, null, x.Nsfw, null, x.Developers, x.Publishers, x.Genres, null, null, null, x.Reviews, 0)).ToListAsync(cancellationToken);
            }

            return games;
        }
    }
}
