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

        public Task<List<Game>> Handle(GetGamesBySearch request, CancellationToken cancellationToken)
        {
            var games = _context.Games.Where(x => EF.Functions.Like(x.Title, $"%{request.SearchString}%")).Take(5).ToList();
            return Task.FromResult(games);
        }
    }
}
