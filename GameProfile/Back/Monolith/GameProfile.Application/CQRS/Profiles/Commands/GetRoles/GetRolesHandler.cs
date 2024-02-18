using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.Commands.GetRoles
{
    public sealed class GetRolesHandler : IRequestHandler<GetRolesRequest, List<ProfileForAdminDTO>>
    {
        private readonly IDatabaseContext _context;

        public GetRolesHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<ProfileForAdminDTO>> Handle(GetRolesRequest request, CancellationToken cancellationToken)
        {
            var profiles = await _context.Profiles.Select(x => new ProfileForAdminDTO(x.Name.Value, x.Roles.FirstOrDefault().Name,x.Id)).ToListAsync(cancellationToken);
            return profiles;
        }
    }
}
