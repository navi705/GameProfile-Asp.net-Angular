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
        
        public Task<List<Game>> Handle(GetGamesByPubOrDevQuery request, CancellationToken cancellationToken)
        {
            List<Game> games = new();
            if (request.type == "developer")
            {
                games = _context.Games.Where(g => g.Developers.Any(d => d.GameString == request.who)).ToList();

            }
            if (request.type == "publisher")
            {
                games = _context.Games.Where(g => g.Publishers.Any(d => d.GameString == request.who)).ToList();
            }

            return Task.FromResult(games);
        }
    }
}
