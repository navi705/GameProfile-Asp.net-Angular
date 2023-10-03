using GameProfile.Application.Data;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.Role.Request.DoesHaveHaveRoleAdmin
{
    public sealed class DoesHaveHaveRoleAdminQueryHandler : IRequestHandler<DoesHaveHaveRoleAdminQuery,bool>
    {
        private readonly IDatabaseContext _context;

        public DoesHaveHaveRoleAdminQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DoesHaveHaveRoleAdminQuery request, CancellationToken cancellationToken)
        {
            var profile = await _context.Profiles.Include(p => p.Roles)
                                      .FirstOrDefaultAsync(p => p.Id == request.ProfileId);
            var hasAdminRole = profile.Roles.Any(r => r.Name == "Admin");

            return hasAdminRole;
        }
    }
}
