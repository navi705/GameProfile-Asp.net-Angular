using MediatR;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Create
{
    public sealed record class CreateReplieCommand(string Content, Guid AuthorId, Guid MessagePostId) : IRequest<GameProfile.Domain.Entities.Forum.Replie>;
}
