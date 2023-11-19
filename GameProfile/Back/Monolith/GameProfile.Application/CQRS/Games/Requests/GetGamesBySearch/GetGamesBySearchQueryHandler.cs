using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesBySearch
{
    public sealed record class GetGamesBySearchQueryHandler : IRequestHandler<GetGamesBySearch, List<GamesForTitleSearchDTO>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesBySearchQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<GamesForTitleSearchDTO>> Handle(GetGamesBySearch request, CancellationToken cancellationToken)
        {
            var games = await _context.Games.AsNoTracking().Where(x => EF.Functions.Like(x.Title.ToLower(), $"%{request.SearchString.ToLower()}%"))
                .Select(x => new GamesForTitleSearchDTO(x.Id, x.Title,x.HeaderImage)).Take(10)
                .ToListAsync(cancellationToken);
            return games;
        }
    }
}
