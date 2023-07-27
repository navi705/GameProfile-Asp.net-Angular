using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.NotSteamGameAppID.Command.Create
{
    public class NotSteamGameAppIDCreateCommandHandler : IRequestHandler<NotSteamGameAppIDCreateCommand>
    {
        private readonly IDatabaseContext _context;

        public NotSteamGameAppIDCreateCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(NotSteamGameAppIDCreateCommand request, CancellationToken cancellationToken)
        {
            var appID = new NotGameSteamId(Guid.Empty,request.AppId);
            _context.NotGameSteamIds.Add(appID);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
