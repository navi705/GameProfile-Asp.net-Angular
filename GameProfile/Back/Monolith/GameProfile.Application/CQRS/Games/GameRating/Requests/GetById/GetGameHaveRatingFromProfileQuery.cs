using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameRating.Requests.GetById
{
    public sealed record class GetGameHaveRatingFromProfileQuery(Guid GameId, Guid ProfileId) : IRequest<GameHasRatingFromProfile>;
}
