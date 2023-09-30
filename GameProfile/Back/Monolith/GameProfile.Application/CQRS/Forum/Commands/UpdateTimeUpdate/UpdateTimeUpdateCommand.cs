using MediatR;

namespace GameProfile.Application.CQRS.Forum.Commands.UpdateTimeUpdate
{
    public sealed record class UpdateTimeUpdateCommand(Guid PostId) : IRequest;
}
