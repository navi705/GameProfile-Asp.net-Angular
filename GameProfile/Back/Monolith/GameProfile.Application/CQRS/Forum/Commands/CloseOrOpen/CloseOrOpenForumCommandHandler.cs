using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Commands.CloseOrOpen
{
    public sealed class CloseOrOpenForumCommandHandler : IRequestHandler<CloseOrOpenForumCommand>
    {
        private readonly IDatabaseContext _context;

        public CloseOrOpenForumCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CloseOrOpenForumCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.Where(x => x.Id == request.Id).FirstOrDefaultAsync();
            post.ChangeOpenStatus(request.Close);
            await _context.SaveChangesAsync();
        }
    }
}
