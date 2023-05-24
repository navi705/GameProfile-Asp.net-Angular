using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.Requests.GetGameByName
{
    public sealed class GetGameByNameQueryHandel : IRequestHandler<GetGameByNameQuery,Game>
    {
        private readonly IDatabaseContext _context;
        public GetGameByNameQueryHandel(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<Game> Handle(GetGameByNameQuery request, CancellationToken cancellationToken)
        {
           return Task.FromResult( _context.Games.Where(x => x.Title == request.name).FirstOrDefault());
        }
    }
}
