using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Shared;
using GameProfile.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Commands.Create
{
    public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<Post>>
    {
        private readonly IDatabaseContext _context;

        public CreatePostCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task<Result<Post>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {    
            List<Game> games = _context.Games.AsNoTracking().Where(game => request.Games.Contains(game.Id.ToString())).ToList();

            var post = Post.Create(request.Title,request.Description, request.Topic,request.AuthorProfileId,request.Languages);
            if (!string.IsNullOrWhiteSpace(post.ErrorMessage))
            {
                return post;
            }

            await _context.Posts.AddAsync(post.Content);
            await _context.SaveChangesAsync(cancellationToken);

            post.Content.GamesAdd(games);
            await _context.SaveChangesAsync(cancellationToken);

            return post;
        }
    }
}
