using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Update
{
    public sealed class UpdateGameReplieCommandHandler : IRequestHandler<UpdateGameReplieCommand, Result<GameCommentHasReplie>>
    {
        private readonly IDatabaseContext _context;

        public UpdateGameReplieCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<GameCommentHasReplie>> Handle(UpdateGameReplieCommand request, CancellationToken cancellationToken)
        {
            var gameReplie = await _context.GameCommentHasReplies.Where(x => x.Id == request.ReplieId && x.ProfileId == request.ProfileId).FirstOrDefaultAsync(cancellationToken);
            var result = gameReplie.Update(request.Replie);
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return result;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return result;
        }
    }
}
