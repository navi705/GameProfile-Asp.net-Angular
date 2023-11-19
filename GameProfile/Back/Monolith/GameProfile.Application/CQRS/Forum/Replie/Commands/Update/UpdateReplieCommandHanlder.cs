using GameProfile.Application.Data;
using GameProfile.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Replie.Commands.Update
{
    public sealed class UpdateReplieCommandHanlder : IRequestHandler<UpdateReplieCommand, Result<Domain.Entities.Forum.Replie>>
    {
        private readonly IDatabaseContext _context;

        public UpdateReplieCommandHanlder(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<Result<Domain.Entities.Forum.Replie>> Handle(UpdateReplieCommand request, CancellationToken cancellationToken)
        {
            var replie = await _context.Replies.Where(x => x.Id == request.ReplieId).FirstOrDefaultAsync(cancellationToken);
            var replieResult = replie.UpdateContent(request.Content);
            if (!string.IsNullOrWhiteSpace(replieResult.ErrorMessage))
            {
                return replieResult;
            }
            await _context.SaveChangesAsync(cancellationToken);
            return replieResult;
        }
    }
}
