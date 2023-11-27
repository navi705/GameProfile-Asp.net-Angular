using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Ranks.Commands
{
    public sealed record class RankUpdateCommand(
            Guid id,
            string rank,
            string rankName,
            string rankImage,
            string? rankMax,
            string? rankNameMax,
            string? rankImageMax) : IRequest;

    public sealed class RankUpdateCommandHandler : IRequestHandler<RankUpdateCommand>
    {
        private readonly IDatabaseContext _context;
        public RankUpdateCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(RankUpdateCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.rankMax))
            {
                
            }
            else
            {

            }
        }
    }
}
