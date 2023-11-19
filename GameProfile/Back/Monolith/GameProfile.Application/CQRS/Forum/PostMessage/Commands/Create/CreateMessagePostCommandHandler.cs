using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Commands.Create
{
    public sealed class CreateMessagePostCommandHandler : IRequestHandler<CreateMessagePostCommand,Result<MessagePost>>
    {
        private readonly IDatabaseContext _context;

        public CreateMessagePostCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<MessagePost>> Handle(CreateMessagePostCommand request, CancellationToken cancellationToken)
        {
            var messagePost = MessagePost.Create(request.Content,request.AuthorId,request.PostId);
            if (!string.IsNullOrWhiteSpace(messagePost.ErrorMessage))
            {
                return messagePost;
            }
            await _context.MessagePosts.AddAsync(messagePost.Content);
            await _context.SaveChangesAsync();
            return messagePost;
        }
    }
}
