using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGameByProfileId
{
    public sealed class GetProfileHasGameByProfileIdQueryHandler : IRequestHandler<GetProfileHasGameByProfileIdQuery, List<ProfileHasGames>>
    {
        private readonly IDatabaseContext _context;
        public GetProfileHasGameByProfileIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public Task<List<ProfileHasGames>> Handle(GetProfileHasGameByProfileIdQuery request, CancellationToken cancellationToken)
        {
            var games = _context.ProfileHasGames.Where(x => x.ProfileId == request.proifleId).ToList();
            return Task.FromResult( games);
        }
    }
}
