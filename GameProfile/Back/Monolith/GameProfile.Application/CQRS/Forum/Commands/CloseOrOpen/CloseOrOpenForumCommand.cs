using MediatR;

namespace GameProfile.Application.CQRS.Forum.Commands.CloseOrOpen
{
    public sealed record class CloseOrOpenForumCommand(Guid Id,bool Close) : IRequest;
}
