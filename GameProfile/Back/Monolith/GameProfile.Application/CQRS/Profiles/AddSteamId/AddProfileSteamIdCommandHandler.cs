using GameProfile.Application.Data;
using GameProfile.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.AddSteamId
{
    public sealed class AddProfileSteamIdCommandHandler : IRequestHandler<AddProfileSteamIdCommand>
    {
        private readonly IDatabaseContext _context;

        public AddProfileSteamIdCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(AddProfileSteamIdCommand request, CancellationToken cancellationToken)
        {
            var steamId = new StringForEntity(request.SteamId);
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken);
            profile.SteamIds.Add(steamId);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
