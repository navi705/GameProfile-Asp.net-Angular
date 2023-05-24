using GameProfile.Application.Data;
using GameProfile.Domain.ValueObjects.Game;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGenres
{
    public sealed class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, List<string>>
    {
        private readonly IDatabaseContext _context;
        public GetGenresQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<string>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        { 
            var genres = _context.Games.AsNoTracking().SelectMany(x=> x.Genres).Select(x => x.GameString).Distinct().ToList();
            return Task.FromResult(genres);
        }
    }
}
