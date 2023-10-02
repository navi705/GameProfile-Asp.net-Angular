using GameProfile.Domain.ValueObjects.Game;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.UpdateGameReviews
{
    public sealed record class UpdateGameReviewsCommand(Guid GameId,Review Review) : IRequest;

}
