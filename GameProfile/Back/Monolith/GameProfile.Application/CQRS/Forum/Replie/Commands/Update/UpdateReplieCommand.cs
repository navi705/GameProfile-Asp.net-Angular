using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Update
{
    public sealed record class UpdateReplieCommand(Guid ReplieId, string Content) : IRequest<Result<Domain.Entities.Forum.Replie>>;
}
