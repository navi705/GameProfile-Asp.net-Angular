using MediatR;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Update
{
    public sealed record class UpdateReplieCommand(Guid ReplieId, string Content) : IRequest<GameProfile.Domain.Entities.Forum.Replie>;
}
