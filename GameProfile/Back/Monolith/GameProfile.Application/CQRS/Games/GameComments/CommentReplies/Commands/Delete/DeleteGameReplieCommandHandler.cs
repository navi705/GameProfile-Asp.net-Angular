using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Delete
{
    public sealed class DeleteGameReplieCommandHandler : IRequestHandler<DeleteGameReplieCommand>
    {
        private readonly IDatabaseContext _context;

        public DeleteGameReplieCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteGameReplieCommand request, CancellationToken cancellationToken)
        {
            await _context.GameCommentHasReplies.Where(replie => replie.Id == request.ReplieId).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
