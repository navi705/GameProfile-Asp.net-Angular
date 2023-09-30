using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Commands.Create
{
    public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand>
    {
        private readonly IDatabaseContext _context;

        public CreatePostCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            List<StringForEntity> lang = request.Languages.Select(s => new StringForEntity(s)).ToList();    
            List<Game> games = _context.Games.AsNoTracking().Where(game => request.Games.Contains(game.Id.ToString())).ToList();
            var post = new Post(Guid.Empty, request.Title, request.Description, request.Topic, request.AuthorProfileId, 0, false, DateTime.Now, DateTime.Now, lang, null, null);
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync(cancellationToken);
            post.Games = games;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
