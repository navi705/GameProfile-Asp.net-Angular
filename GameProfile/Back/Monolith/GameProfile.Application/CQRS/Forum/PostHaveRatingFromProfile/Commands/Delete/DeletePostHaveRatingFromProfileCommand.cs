using MediatR;

namespace GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Delete
{
    public sealed record class DeletePostHaveRatingFromProfileCommand(Guid ProfileId, Guid PostId) : IRequest;
}
