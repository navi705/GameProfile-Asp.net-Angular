using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateProfileHasGame
{
    public sealed class UpdateProfileHasGameCommandHandler : IRequestHandler<UpdateProfileHasGameCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateProfileHasGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateProfileHasGameCommand request, CancellationToken cancellationToken)
        {
            await _context.ExecuteSqlInterpolatedAsync($"update ProfileHasGames set StatusGame = {request.StatusGame}, MinutesInGame ={request.hours * 60} where GameId = {request.gameId} and ProfileId= {request.profileId}", cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
