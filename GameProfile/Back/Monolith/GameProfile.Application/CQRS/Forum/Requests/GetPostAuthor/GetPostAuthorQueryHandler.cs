using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Requests.GetPostAuthor
{
    public sealed class GetPostAuthorQueryHandler : IRequestHandler<GetPostAuthorQuery,string>
    {
        private readonly IDatabaseContext _context;

        public GetPostAuthorQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetPostAuthorQuery request, CancellationToken cancellationToken)
        {
            var author = await _context.Posts.Where(x => x.Id == request.Id).Select(g => g.Author.ToString()).FirstAsync(cancellationToken);
            return author;
        }
    }
}
