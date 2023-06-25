using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGameByName
{
    public sealed class GetGameByNameQueryHandler : IRequestHandler<GetGameByNameQuery,Game?>
    {
        private readonly IDatabaseContext _context;
        public GetGameByNameQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<Game?> Handle(GetGameByNameQuery request, CancellationToken cancellationToken)
        {
           return Task.FromResult( _context.Games.Where(x => x.Title == request.Name).FirstOrDefault());
        }
    }
}
