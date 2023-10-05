using GameProfile.Application.Data;
using GameProfile.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetSteamIdBySteamId
{
    public sealed class GetSteamIdBySteamIdQueryHandler : IRequestHandler<GetSteamIdBySteamIdQuery, bool>
    {
        private readonly IDatabaseContext _context;

        public GetSteamIdBySteamIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(GetSteamIdBySteamIdQuery request, CancellationToken cancellationToken)
        {
            var stringForEntity = new StringForEntity(request.SteamId);
            return await _context.Profiles.AnyAsync(x => x.SteamIds.Any(s => s.StringFor == stringForEntity.StringFor), cancellationToken);
        }
    }
}
