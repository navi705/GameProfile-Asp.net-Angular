using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Requests.GetById
{
    public sealed class GetGameReplieByIdQueryHandler : IRequestHandler<GetGameReplieByIdQuery,GameCommentHasReplie>
    {
        private readonly IDatabaseContext _context;

        public GetGameReplieByIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GameCommentHasReplie> Handle(GetGameReplieByIdQuery request, CancellationToken cancellationToken)
        {
           return await _context.GameCommentHasReplies.AsNoTracking().Where(x => x.Id == request.Replie).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
