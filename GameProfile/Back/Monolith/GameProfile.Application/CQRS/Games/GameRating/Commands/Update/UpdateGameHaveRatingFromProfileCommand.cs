using MediatR;

namespace GameProfile.Application.CQRS.Games.GameRating.Commands.Update
{
    public sealed record class UpdateGameHaveRatingFromProfileCommand(Guid GameId, Guid ProfileId, int score) : IRequest;
}
