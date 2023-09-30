using GameProfile.Domain.Entities.Forum;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostMessage.Requests.GetById
{
    public sealed record class GetPostMessageByIdQuery(Guid PostMessageId) : IRequest<MessagePost>;
}
