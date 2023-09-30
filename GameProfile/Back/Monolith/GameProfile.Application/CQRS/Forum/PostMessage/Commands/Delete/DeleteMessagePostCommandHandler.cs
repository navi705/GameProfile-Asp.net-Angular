using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Commands.Delete
{
    public sealed class DeleteMessagePostCommandHandler : IRequestHandler<DeleteMessagePostCommand>
    {
        private readonly IDatabaseContext _context;

        public DeleteMessagePostCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteMessagePostCommand request, CancellationToken cancellationToken)
        {
            await _context.MessagePosts.Where(x => x.Id == request.MessagePostId).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
