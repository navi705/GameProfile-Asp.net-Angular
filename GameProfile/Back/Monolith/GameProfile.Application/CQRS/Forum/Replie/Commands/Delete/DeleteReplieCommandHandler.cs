using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Delete
{
    public sealed class DeleteReplieCommandHandler : IRequestHandler<DeleteReplieCommand>
    {
        private readonly IDatabaseContext _context;

        public DeleteReplieCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteReplieCommand request, CancellationToken cancellationToken)
        {
            await _context.Replies.Where(x => x.Id == request.ReplieId).ExecuteDeleteAsync(cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
