using GameProfile.Application.Data;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame
{
    public sealed class CreateProfileHasGameCommandHandler : IRequestHandler<CreateProfileHasGameCommand>
    {
        private readonly IDatabaseContext _context;
        public CreateProfileHasGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateProfileHasGameCommand request, CancellationToken cancellationToken)
        {
            var profileHasGame = new ProfileHasGames(Guid.Empty, request.ProfileId, request.GameId, request.StatusGameProgressions, request.MinutesInGame);
            _context.ProfileHasGames.Add(profileHasGame);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
