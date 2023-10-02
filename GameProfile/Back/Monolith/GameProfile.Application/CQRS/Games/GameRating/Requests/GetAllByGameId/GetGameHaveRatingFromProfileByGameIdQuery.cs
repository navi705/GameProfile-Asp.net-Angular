using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameRating.Requests.GetAllByGameId
{
    public sealed record class GetGameHaveRatingFromProfileByGameIdQuery(Guid GameId) : IRequest<List<GameHasRatingFromProfile>>;
}
