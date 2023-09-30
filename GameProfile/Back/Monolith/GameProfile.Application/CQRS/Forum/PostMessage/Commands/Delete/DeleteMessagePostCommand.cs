using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Commands.Delete
{
    public sealed record class DeleteMessagePostCommand(Guid MessagePostId) : IRequest;
}
