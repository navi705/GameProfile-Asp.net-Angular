using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameRating.Commands.Delete
{
    public sealed class DeleteGameHaveRatingFromProfileCommandHandler : IRequestHandler<DeleteGameHaveRatingFromProfileCommand>
    {
        private readonly IDatabaseContext _context;

        public DeleteGameHaveRatingFromProfileCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteGameHaveRatingFromProfileCommand request, CancellationToken cancellationToken)
        {
            await _context.GameHasRatingFromProfiles.Where(x => x.ProfileId == request.ProfileId && x.GameId == request.GameId).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
