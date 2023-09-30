using MediatR;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Delete
{
    public sealed record class DeleteReplieCommand(Guid ReplieId) : IRequest;
}
