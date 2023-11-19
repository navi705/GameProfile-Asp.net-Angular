using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Update
{
    public sealed class UpdatePostHaveRatingFromProfileCommandHandler : IRequestHandler<UpdatePostHaveRatingFromProfileCommand>
    {
        private readonly IDatabaseContext _context;

        public UpdatePostHaveRatingFromProfileCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdatePostHaveRatingFromProfileCommand request, CancellationToken cancellationToken)
        {
            var postHaveRating = await _context.PostHaveRatingFromProfiles.Where(x => x.PostId == request.PostId && x.ProfileId == request.ProfileId).FirstOrDefaultAsync(cancellationToken);
            postHaveRating.IsPositiveEdit(request.IsPositive);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
