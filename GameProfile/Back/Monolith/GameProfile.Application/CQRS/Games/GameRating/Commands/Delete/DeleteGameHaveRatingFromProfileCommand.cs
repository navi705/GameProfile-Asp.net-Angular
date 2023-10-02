using MediatR;

namespace GameProfile.Application.CQRS.Games.GameRating.Commands.Delete
{
    public sealed record class DeleteGameHaveRatingFromProfileCommand(Guid GameId, Guid ProfileId) : IRequest;
}
