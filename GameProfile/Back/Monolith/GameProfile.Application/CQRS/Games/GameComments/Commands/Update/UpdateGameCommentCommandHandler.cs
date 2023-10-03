using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Update
{
    public sealed class UpdateGameCommentCommandHandler : IRequestHandler<UpdateGameCommentCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateGameCommentCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateGameCommentCommand request, CancellationToken cancellationToken)
        {
            var gameComment = await _context.GameHasComments.Where(x => x.Id == request.CommentId).FirstOrDefaultAsync(cancellationToken);     
            if(gameComment.ProfileId != request.ProfileId)
            {
                return;
            }
            gameComment.Comment = request.Comment;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
