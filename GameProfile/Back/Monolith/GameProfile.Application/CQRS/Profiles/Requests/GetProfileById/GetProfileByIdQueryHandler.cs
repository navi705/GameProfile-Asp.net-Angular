using GameProfile.Application.CQRS.Profiles.Requests.GetBySteamId;
using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetProfileById
{
    public sealed class GetProfileByIdQueryHandler : IRequestHandler<GetProfileByIdQuery, Profile>
    {
        private readonly IDatabaseContext _context;

        public GetProfileByIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<Profile> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Profiles.Where(x => x.Id == request.id).FirstOrDefault();
            return Task.FromResult(query);
        }
    }
}
