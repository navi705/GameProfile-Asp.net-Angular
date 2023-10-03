using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Delete
{
    public sealed class DeleteGameCommentCommandHandler : IRequestHandler<DeleteGameCommentCommand>
    {
        private readonly IDatabaseContext _context;

        public DeleteGameCommentCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteGameCommentCommand request, CancellationToken cancellationToken)
        {
            await _context.GameHasComments.Where(comment => comment.Id == request.CommentId).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
