using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileSteamIds
{
    public sealed class GetProfileSteamIdsQueryHandler : IRequestHandler<GetProfileSteamIdsQuery, List<string>>
    {
        private readonly IDatabaseContext _context;

        public GetProfileSteamIdsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<string>> Handle(GetProfileSteamIdsQuery request, CancellationToken cancellationToken)
        {
            var steamIds = await _context.Profiles.AsNoTracking().Where(x => x.Id == request.ProfileId).SelectMany(x => x.SteamIds).ToListAsync(cancellationToken);
            return steamIds.Select(x => x.StringFor).ToList();
        }
    }
}
