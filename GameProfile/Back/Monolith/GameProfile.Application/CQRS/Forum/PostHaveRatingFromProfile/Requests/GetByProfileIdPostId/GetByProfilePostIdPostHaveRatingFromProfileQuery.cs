using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Requests.GetByProfileIdPostId
{
    public sealed record class GetByProfilePostIdPostHaveRatingFromProfileQuery(Guid PostId, Guid ProfileId) : IRequest<GameProfile.Domain.Entities.Forum.PostHaveRatingFromProfile>;
}
