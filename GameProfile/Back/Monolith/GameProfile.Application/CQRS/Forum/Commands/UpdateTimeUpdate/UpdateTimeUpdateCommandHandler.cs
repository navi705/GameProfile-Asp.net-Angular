using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Commands.UpdateTimeUpdate
{
    public sealed class UpdateTimeUpdateCommandHandler : IRequestHandler<UpdateTimeUpdateCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateTimeUpdateCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateTimeUpdateCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.Where(x => x.Id == request.PostId).FirstOrDefaultAsync(cancellationToken);
            post.UpdateUpdateTime();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
