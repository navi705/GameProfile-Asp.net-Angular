using GameProfile.Application.Data;
using GameProfile.Domain.Entities.ProfileEntites;
using GameProfile.Domain.ValueObjects;
using GameProfile.Domain.ValueObjects.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Commands
{
    public sealed class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand>
    {
        private readonly IDatabaseContext _context;

        public CreateProfileCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            var name = Name.Create(request.Name);
            var description = Description.Create(request.Description);
            var steamId = new List<StringForEntity>
            {
                new StringForEntity(request.SteamId)
            };
            var profile = new Profile(Guid.Empty, name, description, steamId);
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
