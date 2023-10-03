using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.Requests.GetById
{
    public sealed class GetGameCommentByIdQueryHandler : IRequestHandler<GetGameCommentByIdQuery,GameHasComments>
    {
        private readonly IDatabaseContext _context;

        public GetGameCommentByIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GameHasComments> Handle(GetGameCommentByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.GameHasComments.AsNoTracking().Where(x => x.Id == request.CommentId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
