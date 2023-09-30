using GameProfile.Application.CQRS.Games.Commands.DeleteGame;
using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Commands.Delete
{
    public sealed class DeleteForumCommandHandler : IRequestHandler<DeleteForumCommand>
    {
        private readonly IDatabaseContext _context;

        public DeleteForumCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteForumCommand request, CancellationToken cancellationToken)
        {
            await _context.Posts.Where(x => x.Id == request.ForumId).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
