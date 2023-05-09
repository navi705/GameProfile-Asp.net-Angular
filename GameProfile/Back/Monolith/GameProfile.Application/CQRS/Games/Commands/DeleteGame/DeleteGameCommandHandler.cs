using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand>
    {
        private readonly IDatabaseContext _context;

        public DeleteGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            await _context.ExecuteSqlRawAsync($"DELETE FROM Games WHERE Id = '{request.gameId}'",cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
