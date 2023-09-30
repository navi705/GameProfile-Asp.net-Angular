using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Requests.GetByProfileIdPostId
{
    public sealed class GetByProfilePostIdPostHaveRatingFromProfileQueryHandler : IRequestHandler<GetByProfilePostIdPostHaveRatingFromProfileQuery, GameProfile.Domain.Entities.Forum.PostHaveRatingFromProfile>
    {
        private readonly IDatabaseContext _context;

        public GetByProfilePostIdPostHaveRatingFromProfileQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<GameProfile.Domain.Entities.Forum.PostHaveRatingFromProfile> Handle(GetByProfilePostIdPostHaveRatingFromProfileQuery request, CancellationToken cancellationToken)
        {
            var postHaveRating = await _context.PostHaveRatingFromProfiles.Where(x => x.PostId == request.PostId && x.ProfileId == request.ProfileId).FirstOrDefaultAsync(cancellationToken);
            return postHaveRating;
        }
    }
}
