using GameProfile.Domain.Entities.Forum;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.Requests.GetPostById
{
    public sealed record class GetPostByIdQuery(Guid PostId) : IRequest<Post>;
}
