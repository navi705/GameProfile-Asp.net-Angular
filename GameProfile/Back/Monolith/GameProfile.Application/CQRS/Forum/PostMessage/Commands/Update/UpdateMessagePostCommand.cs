using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Commands.Update
{
    public sealed record class UpdateMessagePostCommand(Guid MessagePostId,string Content) : IRequest<Result<MessagePost>>;
}
