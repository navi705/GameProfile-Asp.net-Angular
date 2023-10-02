using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetTags
{
    public sealed record class GetTagsQueryHandler : IRequestHandler<GetTagsQuery,List<string>>
    {
        private readonly IDatabaseContext _context;
        private readonly ICacheService _cacheService;
        public GetTagsQueryHandler(IDatabaseContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<List<string>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            //var tags = _context.Games.AsNoTracking().SelectMany(x => x.Tags).Select(x => x.GameString).Distinct().OrderBy(x => x).ToListAsync(cancellationToken);
            var tags = await _cacheService.GetAsync<List<string>>("tags");
            return tags;
        }
    }
}
