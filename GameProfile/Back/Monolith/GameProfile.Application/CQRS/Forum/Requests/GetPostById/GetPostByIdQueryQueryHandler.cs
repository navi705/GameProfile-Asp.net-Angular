using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using GameProfile.Domain.Entities.Forum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Requests.GetPostById
{
    public sealed class GetPostByIdQueryQueryHandler : IRequestHandler<GetPostByIdQuery, PostOneDTO>
    {
        private readonly IDatabaseContext _context;

        public GetPostByIdQueryQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<PostOneDTO> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.AsNoTracking().Include(x => x.Games)
                .Include(x => x.Profile).Include(p => p.MessagePosts).ThenInclude(mp => mp.Profile)
                .Include(p => p.MessagePosts).ThenInclude(mp => mp.Replies).ThenInclude(mg => mg.Profile)
                .Where(x => x.Id == request.PostId)
                .Select(x => new PostOneDTO(x.Id, x.Title, x.Description, x.Topic, x.Rating, x.Closed, x.Created, x.Updated, 
                new PostAuthorDTO(x.Author, x.Profile.Name.Value), x.Languages.Select(xa => xa.StringFor).ToList(), x.Games.Select(gp => new PostGameDTO(gp.Id, gp.Title)).ToList(),
                x.MessagePosts.Select(gx=> new PostMessagesDTO(gx.Id, gx.Content, gx.Created, new PostAuthorDTO(gx.AuthorId, gx.Profile.Name.Value)
                ,gx.Replies.Select(gp=> new PostMessagesRepliesDTO(gp.Id, gp.Content,gp.Created,new PostAuthorDTO(gp.AuthorId, gp.Profile.Name.Value))).ToList())).ToList())).ToListAsync(cancellationToken);

            return post[0];
        }
    }
}
