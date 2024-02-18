using GameProfile.Application.Data;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.Commands.AddRoleToProfile
{
    public sealed class AddRoleToProfileCommandHandler : IRequestHandler<AddRoleToProfileCommand>
    {
        private readonly IDatabaseContext _context;

        public AddRoleToProfileCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(AddRoleToProfileCommand request, CancellationToken cancellationToken)
        {
            var profile1 = await _context.Profiles.Include(x=>x.Roles).FirstAsync(x => x.Id == request.ProfileId, cancellationToken);

            if (profile1.Roles != null)
            {
                var asd = profile1.Roles.Any(x => x.Name == "Moderator");


                if (asd)
                {
                    var asdf = profile1.Roles.Where(x => x.Name == "Moderator").First();
                    profile1.Roles.Remove(asdf);
                    await _context.SaveChangesAsync();
                    return;
                }
            }

            var IsModeratorHave = await _context.Roles.AnyAsync(x => x.Name == "Moderator",cancellationToken);

           

            if (!IsModeratorHave)
            {
                GameProfile.Domain.Entities.ProfileEntites.Role role = new Domain.Entities.ProfileEntites.Role(Guid.Empty, "Moderator", null);
                await _context.Roles.AddAsync(role,cancellationToken);
                await _context.SaveChangesAsync();
            }

            var profile = await _context.Profiles.FirstAsync(x => x.Id == request.ProfileId,cancellationToken);

            var roleModer = await _context.Roles.FirstOrDefaultAsync(x => x.Name == "Moderator");

            if (profile.Roles == null)
            {
                profile.Roles = new List<GameProfile.Domain.Entities.ProfileEntites.Role>();
            }

            profile.Roles.Add(roleModer);

            await _context.SaveChangesAsync();

        }
    }
}
