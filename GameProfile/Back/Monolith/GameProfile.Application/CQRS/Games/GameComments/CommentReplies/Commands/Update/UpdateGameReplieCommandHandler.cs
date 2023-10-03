using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Update
{
    public sealed class UpdateGameReplieCommandHandler : IRequestHandler<UpdateGameReplieCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateGameReplieCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateGameReplieCommand request, CancellationToken cancellationToken)
        {
            var gameReplie = await _context.GameCommentHasReplies.Where(x => x.Id == request.ReplieId && x.ProfileId == request.ProfileId).FirstOrDefaultAsync(cancellationToken);
            gameReplie.Replie = request.Replie;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
