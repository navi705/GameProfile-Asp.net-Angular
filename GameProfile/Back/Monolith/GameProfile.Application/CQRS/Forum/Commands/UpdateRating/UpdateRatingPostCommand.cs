using MediatR;

namespace GameProfile.Application.CQRS.Forum.Commands.UpdateRating
{
    public sealed record class UpdateRatingPostCommand(Guid PostId, int Rating) : IRequest;
}
