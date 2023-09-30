using MediatR;

namespace GameProfile.Application.CQRS.Forum.Commands.Delete
{
    public sealed record class DeleteForumCommand (Guid ForumId ) : IRequest;
}
