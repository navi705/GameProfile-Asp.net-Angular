using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Role.Command.CreateRole
{
    public sealed record class CreateRoleCommand(string Name): IRequest;
}
