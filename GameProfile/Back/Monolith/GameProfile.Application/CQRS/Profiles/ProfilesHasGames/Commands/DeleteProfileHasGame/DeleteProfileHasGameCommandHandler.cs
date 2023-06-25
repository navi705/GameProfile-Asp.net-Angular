using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            await _context.ProfileHasGames.Where(x => x.GameId == request.GameId && x.ProfileId == request.ProfileId).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
