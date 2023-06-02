using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.DeleteProfileHasGame
{
    public sealed class DeleteProfileHasGameCommandHandler : IRequestHandler<DeleteProfileHasGameCommand>
    {
        private readonly IDatabaseContext _context;

        public DeleteProfileHasGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteProfileHasGameCommand request, CancellationToken cancellationToken)
        {
            await _context.ExecuteSqlRawAsync($"DELETE FROM ProfileHasGames WHERE GameId = '{request.gameId}' and ProfileId = '{request.userId}' ", cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
