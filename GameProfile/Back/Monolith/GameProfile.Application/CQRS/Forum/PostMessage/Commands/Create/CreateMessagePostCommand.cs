using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Commands.Create
{
    public sealed record class CreateMessagePostCommand(string Content, Guid AuthorId, Guid PostId) : IRequest<Result<MessagePost>>;
}
