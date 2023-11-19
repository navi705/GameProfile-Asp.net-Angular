using GameProfile.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Commands.UpdateRatingComplitation
{
    public sealed class UpdateRatingComplitationCommandHandler : IRequestHandler<UpdateRatingComplitationCommand>
    {
        private readonly IDatabaseContext _context;
        public UpdateRatingComplitationCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateRatingComplitationCommand request, CancellationToken cancellationToken)
        {
            var postHaveRating = await _context.PostHaveRatingFromProfiles.Where(x => x.PostId == request.PostId && x.ProfileId == request.ProfileId).FirstOrDefaultAsync(cancellationToken);
            bool isPositive = false;
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == request.PostId, cancellationToken);
            if(post == null) 
            {
                throw new ArgumentException("Post null how is ?");
            }

            if (request.Rating == "positive")
            {
                isPositive = true;
            }
            else
            {
                isPositive = false;
            }

            if (postHaveRating is null)
            {
                if (request.Rating == "positive")
                {
                    await _context.PostHaveRatingFromProfiles.AddAsync(new Domain.Entities.Forum.PostHaveRatingFromProfile(Guid.NewGuid(), request.ProfileId, request.PostId, isPositive), cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
         
                    post.ChangeRating(1);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                else
                {                 
                    await _context.PostHaveRatingFromProfiles.AddAsync(new Domain.Entities.Forum.PostHaveRatingFromProfile(Guid.NewGuid(), request.ProfileId, request.PostId, isPositive), cancellationToken);
                    await _context.SaveChangesAsync(cancellationToken);
                
                    post.ChangeRating(-1);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            else
            {

                if (request.Rating == "positive")
                {
                    if (postHaveRating.IsPositive == false)
                    {
                        postHaveRating.IsPositiveEdit(isPositive);
                        await _context.SaveChangesAsync(cancellationToken);
                       
                        post.ChangeRating(2);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        await _context.PostHaveRatingFromProfiles.Where(x => x.PostId == request.PostId && x.ProfileId == request.ProfileId).ExecuteDeleteAsync(cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        post.ChangeRating(-1);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
                else
                {
                    if (postHaveRating.IsPositive == true)
                    {
                        postHaveRating.IsPositiveEdit(isPositive);
                        await _context.SaveChangesAsync(cancellationToken);

                        post.ChangeRating(-2);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        await _context.PostHaveRatingFromProfiles.Where(x => x.PostId == request.PostId && x.ProfileId == request.ProfileId).ExecuteDeleteAsync(cancellationToken);
                        await _context.SaveChangesAsync(cancellationToken);

                        post.ChangeRating(1);
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                }
            }

        }
    }
}
