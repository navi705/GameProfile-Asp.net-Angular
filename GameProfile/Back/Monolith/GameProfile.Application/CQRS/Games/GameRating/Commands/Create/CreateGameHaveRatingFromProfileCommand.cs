using MediatR;

namespace GameProfile.Application.CQRS.Games.GameRating.Commands.Create
{
    public sealed record class CreateGameHaveRatingFromProfileCommand(Guid GameId, Guid ProfileId, int score) : IRequest;
}
