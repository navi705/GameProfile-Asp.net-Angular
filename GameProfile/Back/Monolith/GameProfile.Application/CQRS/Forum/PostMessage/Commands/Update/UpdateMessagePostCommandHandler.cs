using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Commands.Update
{
    public sealed class UpdateMessagePostCommandHandler : IRequestHandler<UpdateMessagePostCommand,Result<MessagePost>>
    {
        private readonly IDatabaseContext _context;

        public UpdateMessagePostCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<MessagePost>> Handle(UpdateMessagePostCommand request, CancellationToken cancellationToken)
        {
            var postMessage = await _context.MessagePosts.Where(x => x.Id == request.MessagePostId).FirstOrDefaultAsync();
            var result = postMessage.UpdateContent(request.Content);
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return result;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
