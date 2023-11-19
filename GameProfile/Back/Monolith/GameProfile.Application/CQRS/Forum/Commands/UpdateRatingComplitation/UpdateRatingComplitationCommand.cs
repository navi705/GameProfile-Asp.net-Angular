using MediatR;

namespace GameProfile.Application.CQRS.Forum.Commands.UpdateRatingComplitation
{
    public sealed record class UpdateRatingComplitationCommand(Guid ProfileId, Guid PostId, string Rating) : IRequest;
}
