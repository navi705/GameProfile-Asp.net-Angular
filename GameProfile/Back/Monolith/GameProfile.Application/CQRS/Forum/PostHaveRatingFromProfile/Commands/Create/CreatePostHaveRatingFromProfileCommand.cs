using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Create
{
    public sealed record class CreatePostHaveRatingFromProfileCommand(Guid PostId, Guid ProfileId,bool IsPositive) : IRequest;
}
