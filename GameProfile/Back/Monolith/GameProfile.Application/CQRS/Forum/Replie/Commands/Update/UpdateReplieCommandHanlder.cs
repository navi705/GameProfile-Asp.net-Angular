using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Update
{
    public sealed class UpdateReplieCommandHanlder : IRequestHandler<UpdateReplieCommand, GameProfile.Domain.Entities.Forum.Replie>
    {
        private readonly IDatabaseContext _context;

        public UpdateReplieCommandHanlder(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GameProfile.Domain.Entities.Forum.Replie> Handle(UpdateReplieCommand request, CancellationToken cancellationToken)
        {
            var replie = await _context.Replies.Where(x => x.Id == request.ReplieId).FirstOrDefaultAsync(cancellationToken);
            replie.Content = request.Content;
            await _context.SaveChangesAsync(cancellationToken);
            return replie;
        }
    }
}
