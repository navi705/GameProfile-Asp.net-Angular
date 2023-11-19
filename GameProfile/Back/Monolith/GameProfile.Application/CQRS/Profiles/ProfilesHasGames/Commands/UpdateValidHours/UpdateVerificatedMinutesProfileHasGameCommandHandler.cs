using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateValidHours;
using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateProfileHasGame
{
    public sealed class UpdateVerificatedMinutesProfileHasGameCommandHandler : IRequestHandler<UpdateVerificatedMinutesProfileHasGameCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateVerificatedMinutesProfileHasGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateVerificatedMinutesProfileHasGameCommand request, CancellationToken cancellationToken)
        {
            var profileHasGame = await _context.ProfileHasGames.FirstOrDefaultAsync(x => x.ProfileId == request.ProfileId && x.GameId == request.GameId, cancellationToken);
            profileHasGame.ChangeVerificatedHours(request.Minutes);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
