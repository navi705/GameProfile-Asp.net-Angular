using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Commands.Update
{
    public sealed class UpdateMessagePostCommandHandler : IRequestHandler<UpdateMessagePostCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateMessagePostCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateMessagePostCommand request, CancellationToken cancellationToken)
        {
            var postMessage = await _context.MessagePosts.Where(x => x.Id == request.MessagePostId).FirstOrDefaultAsync();
            postMessage.Content = request.Content;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
