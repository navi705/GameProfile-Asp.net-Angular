using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.NotSteamGameAppID.Requests
{
    public sealed class NotSteamGameAppIdQueryHandler : IRequestHandler<NotSteamGameAppIdQuery,bool>
    {
        private readonly IDatabaseContext _context;

        public NotSteamGameAppIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(NotSteamGameAppIdQuery request, CancellationToken cancellationToken)
        {
            var app = await _context.NotGameSteamIds.AsNoTracking().FirstOrDefaultAsync(x=>x.SteamAppId == request.AppId,cancellationToken);
            if (app == null)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
    }
}
