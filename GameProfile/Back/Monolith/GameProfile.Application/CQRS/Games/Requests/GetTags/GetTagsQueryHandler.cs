using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetTags
{
    public sealed record class GetTagsQueryHandler : IRequestHandler<GetTagsQuery,List<string>>
    {
        private readonly IDatabaseContext _context;
        public GetTagsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<string>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = _context.Games.AsNoTracking().SelectMany(x => x.Tags).Select(x => x.GameString).Distinct().OrderBy(x => x).ToListAsync(cancellationToken);
            return tags;
        }
    }
}
