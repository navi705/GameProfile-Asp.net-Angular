using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetBySteamId
{
    public sealed class GetProfileQueryHandler : IRequestHandler<GetProfileQuery,Profile>
    {
        private readonly IDatabaseContext _context;

        public GetProfileQueryHandler(IDatabaseContext context) 
        {
            _context = context;
        }

        public Task<Profile?> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
          var query = _context.Profiles.Where(x => x.SteamIds.Any(g => g.StringFor.Contains(request.steamId))).FirstOrDefault();
          return Task.FromResult(query);

        }
    }
}
