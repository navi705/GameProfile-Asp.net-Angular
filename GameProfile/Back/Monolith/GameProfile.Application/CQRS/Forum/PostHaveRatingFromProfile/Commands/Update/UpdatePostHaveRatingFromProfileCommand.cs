using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Update
{
    public sealed record class UpdatePostHaveRatingFromProfileCommand(Guid PostId, Guid ProfileId, bool IsPositive) : IRequest;
}
