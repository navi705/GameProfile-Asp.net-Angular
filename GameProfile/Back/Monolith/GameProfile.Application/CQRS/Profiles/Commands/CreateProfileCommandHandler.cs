using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Profile;
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
            var name = Name.Create(request.name);
            var description = Description.Create(request.description);
            var steamId = new List<StringForEntity>();
            steamId.Add(new StringForEntity( request.steamId));
            var profile = new Profile(Guid.Empty, name, description, steamId);
            _context.Profiles.Add(profile);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
