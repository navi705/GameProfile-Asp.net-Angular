using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var recordId = _context.ProfileHasGames.Where(x => x.GameId == request.GameId && x.ProfileId == request.ProfileId).Select(x => x.Id).FirstOrDefault();
            await _context.ProfileHasGames.Where(x => x.GameId == request.GameId && x.ProfileId == request.ProfileId).ExecuteDeleteAsync(cancellationToken);
            var profileHasGame = new ProfileHasGames(recordId, request.ProfileId,request.GameId,request.StatusGame, request.Hours * 60);
            _context.ProfileHasGames.Add(profileHasGame);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
