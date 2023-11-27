using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Ranks.Commands
{
    public sealed record class RankCreateCommand(
            Guid gameId,
            Guid profileId,
            string rank,
            string rankName,
            string rankImage,
            string? rankMax,
            string? rankNameMax,
            string? rankImageMax) : IRequest;

    public sealed class RankCreateCommandHandler : IRequestHandler<RankCreateCommand>
    {
        private readonly IDatabaseContext _context;
        public RankCreateCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(RankCreateCommand request, CancellationToken cancellationToken)
        {
            var rank = new GameProfile.Domain.Entities.ProfileEntites.Ranks(Guid.Empty, request.gameId, request.profileId, request.rank, request.rankName, request.rankImage, request.rankMax, request.rankNameMax, request.rankImageMax);
            _context.Ranks.Add(rank);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
