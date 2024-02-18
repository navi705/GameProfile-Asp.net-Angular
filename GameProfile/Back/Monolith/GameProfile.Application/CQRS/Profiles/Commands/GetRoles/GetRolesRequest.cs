using GameProfile.Application.DTO;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Commands.GetRoles
{
    public sealed record class GetRolesRequest() : IRequest<List<ProfileForAdminDTO>>;
}
