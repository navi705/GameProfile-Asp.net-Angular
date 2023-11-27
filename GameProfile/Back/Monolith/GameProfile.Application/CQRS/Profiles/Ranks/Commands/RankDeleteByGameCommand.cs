using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.Ranks.Commands
{
    public sealed record class RankDeleteByGameCommand(Guid GameId, Guid ProfileId) : IRequest;

    public sealed class RankDeleteByGameComandHandler : IRequestHandler<RankDeleteByGameCommand>
    {
        private readonly IDatabaseContext _context;

        public RankDeleteByGameComandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(RankDeleteByGameCommand request, CancellationToken cancellationToken)
        {
            var ranks = await _context.Ranks.Where(x => x.GameId == request.GameId && x.ProfileId == request.ProfileId).ToListAsync(cancellationToken);
            _context.Ranks.RemoveRange(ranks);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
