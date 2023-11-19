using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGenres
{
    public sealed class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, List<string>>
    {
        private readonly IDatabaseContext _context;
        private readonly ICacheService _cacheService;
        public GetGenresQueryHandler(IDatabaseContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<List<string>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
        {
            var genres = await _cacheService.GetAsync<List<string>>("genres");
            return genres;
        }
    }
}
