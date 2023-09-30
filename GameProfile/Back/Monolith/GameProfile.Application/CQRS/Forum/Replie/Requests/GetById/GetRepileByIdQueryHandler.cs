using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Replie.Requests.GetById
{
    public sealed class GetRepileByIdQueryHandler : IRequestHandler<GetRepileByIdQuery,GameProfile.Domain.Entities.Forum.Replie>
    {
        private readonly IDatabaseContext _context;

        public GetRepileByIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GameProfile.Domain.Entities.Forum.Replie> Handle(GetRepileByIdQuery request, CancellationToken cancellationToken)
        {
            var replie = await _context.Replies.AsNoTracking().Where(x => x.Id == request.ReplieId).FirstOrDefaultAsync(cancellationToken);
            return replie;
        }
    }
}
