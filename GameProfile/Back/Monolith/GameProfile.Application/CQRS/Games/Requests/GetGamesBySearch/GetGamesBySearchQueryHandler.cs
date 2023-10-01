using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesBySearch
{
    public sealed record class GetGamesBySearchQueryHandler : IRequestHandler<GetGamesBySearch, List<Game>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesBySearchQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<Game>> Handle(GetGamesBySearch request, CancellationToken cancellationToken)
        {
            var games = await _context.Games.AsNoTracking().Where(x => EF.Functions.Like(x.Title, $"%{request.SearchString}%"))
                .Select(x => new Game(x.Id, x.Title, DateTime.UtcNow, x.HeaderImage, null, false, null, null, null, null, null, null, null, null, 0)).Take(5)
                .ToListAsync(cancellationToken);
            return games;
        }
    }
}
