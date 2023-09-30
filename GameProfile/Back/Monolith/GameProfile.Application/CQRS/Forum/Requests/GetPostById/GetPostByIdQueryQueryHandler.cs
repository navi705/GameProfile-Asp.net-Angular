using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Requests.GetPostById
{
    public sealed class GetPostByIdQueryQueryHandler : IRequestHandler<GetPostByIdQuery, Post>
    {
        private readonly IDatabaseContext _context;

        public GetPostByIdQueryQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.AsNoTracking().Include(x => x.Games)
                .Include(x => x.Profile).Include(p => p.MessagePosts).ThenInclude(mp => mp.Profile)
                .Include(p => p.MessagePosts).ThenInclude(mp => mp.Replies).ThenInclude(mg => mg.Profile)
                .Where(x => x.Id == request.PostId)
                .Select(x => new Post(x.Id, x.Title, x.Description, x.Topic, x.Author, x.Rating, x.Closed, x.Created, x.Updated, x.Languages,
                x.Games.Select(gp => new Game(gp.Id, gp.Title, gp.ReleaseDate, null, null, false, null, null, null, null, null, null, null, null, 0)).ToList(),
                x.MessagePosts.Select(g => new MessagePost(g.Id, g.Content, g.Created, g.AuthorId, Guid.Empty)
                {
                    Profile = new Profile(Guid.Empty, g.Profile.Name, null, null),
                    Replies = g.Replies.Select(h => new GameProfile.Domain.Entities.Forum.Replie(h.Id, h.Content, h.Created, h.AuthorId, h.MessagePostId) { Profile = new Profile(Guid.Empty, h.Profile.Name, null, null) }).ToList()
                }).ToList())
                { Profile = new Profile(x.Author, x.Profile.Name, null, null) }
                )
                .ToListAsync();
            return post[0];
        }
    }
}
