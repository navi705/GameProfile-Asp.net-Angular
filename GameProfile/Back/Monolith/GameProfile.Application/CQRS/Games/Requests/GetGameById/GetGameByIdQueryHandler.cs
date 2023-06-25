using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.Requests
{
    public sealed class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery,Game?>
    {
        private readonly IDatabaseContext _context;

        public GetGameByIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Game?> Handle(GetGameByIdQuery request, CancellationToken cancellationToken)
        {
            var game = await _context.Games.FindAsync(request.GameId,cancellationToken);
            return game;
        }
    }
}
