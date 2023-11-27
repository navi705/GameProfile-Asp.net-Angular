using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.Ranks.Requests
{
    public sealed record class GetRanksQuery(Guid ProfileId) :IRequest<List<RankDTO>>;

    public sealed class GetRanksQueryHandler : IRequestHandler<GetRanksQuery, List<RankDTO>>
    {
        private readonly IDatabaseContext _context;
        public GetRanksQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<RankDTO>> Handle(GetRanksQuery request, CancellationToken cancellationToken)
        {
            //return await _context.Ranks.Where(x => x.ProfileId == request.ProfileId).ToListAsync(cancellationToken);
           return await _context.Ranks.AsNoTracking().Where(x => x.ProfileId == request.ProfileId).Select(x=>
            new RankDTO(x,x.GameId,x.Game.Title)
            ).ToListAsync(cancellationToken);
        }
    }

}
