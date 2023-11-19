using MediatR;

namespace GameProfile.Application.CQRS.Games.GameRating.Commands.GameRatingComp
{
    public sealed record class GameRatingCompCommand(Guid GameId, Guid ProfileId, int Score) : IRequest;
}
