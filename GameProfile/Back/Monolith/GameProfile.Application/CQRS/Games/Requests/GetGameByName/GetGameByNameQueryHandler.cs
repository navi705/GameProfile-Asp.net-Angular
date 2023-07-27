using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGameByName
{
    public sealed class GetGameByNameQueryHandler : IRequestHandler<GetGameByNameQuery,Game?>
    {
        private readonly IDatabaseContext _context;
        public GetGameByNameQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Game?> Handle(GetGameByNameQuery request, CancellationToken cancellationToken)
        {
            return await _context.Games.AsNoTracking().FirstOrDefaultAsync(x => x.Title == request.Name);
        }
    }
}
