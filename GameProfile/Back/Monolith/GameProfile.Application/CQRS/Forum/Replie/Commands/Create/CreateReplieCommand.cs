using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Create
{
    public sealed record class CreateReplieCommand(string Content, Guid AuthorId, Guid MessagePostId) : IRequest<Result<GameProfile.Domain.Entities.Forum.Replie>>;
}
