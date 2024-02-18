using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Commands.AddRoleToProfile
{
    public sealed record class AddRoleToProfileCommand(Guid ProfileId) : IRequest;
}
