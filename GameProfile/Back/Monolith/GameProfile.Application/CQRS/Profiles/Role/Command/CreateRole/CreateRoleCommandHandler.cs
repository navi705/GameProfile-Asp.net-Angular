using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Role.Command.CreateRole
{
    public sealed class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
    {
        private readonly IDatabaseContext _context;

        public CreateRoleCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new GameProfile.Domain.Entities.ProfileEntites.Role(Guid.Empty,request.Name,null);
            await _context.Roles.AddAsync(role,cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
