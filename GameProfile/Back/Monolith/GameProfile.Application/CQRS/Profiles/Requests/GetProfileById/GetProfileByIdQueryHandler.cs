using GameProfile.Application.Data;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetProfileById
{
    public sealed class GetProfileByIdQueryHandler : IRequestHandler<GetProfileByIdQuery, Profile?>
    {
        private readonly IDatabaseContext _context;

        public GetProfileByIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Profile?> Handle(GetProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var query = await _context.Profiles.AsNoTracking().Where(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
            return query;
        }
    }
}
