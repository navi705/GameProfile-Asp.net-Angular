using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Commands.UpdateRating
{
    public sealed class UpdateRatingPostCommandhandler : IRequestHandler<UpdateRatingPostCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdateRatingPostCommandhandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateRatingPostCommand request, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);
            post.Rating += request.Rating;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
