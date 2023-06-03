using GameProfile.Application.Data;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats.GetCount
{
    public class GetCountProfilesQueryHandler : IRequestHandler<GetCountProfilesQuery, int>
    {
        private readonly IDatabaseContext _context;
        public GetCountProfilesQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<int> Handle(GetCountProfilesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_context.Profiles.Count());
        }
    }
}
