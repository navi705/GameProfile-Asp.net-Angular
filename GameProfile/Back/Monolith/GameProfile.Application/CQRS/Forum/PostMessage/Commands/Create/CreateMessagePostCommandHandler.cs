using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Forum;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Commands.Create
{
    public sealed class CreateMessagePostCommandHandler : IRequestHandler<CreateMessagePostCommand>
    {
        private readonly IDatabaseContext _context;

        public CreateMessagePostCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateMessagePostCommand request, CancellationToken cancellationToken)
        {
            var messagePost = new MessagePost(Guid.Empty,request.Content, DateTime.Now, request.AuthorId, request.PostId);
            await _context.MessagePosts.AddAsync(messagePost);
            await _context.SaveChangesAsync();
        }
    }
}
