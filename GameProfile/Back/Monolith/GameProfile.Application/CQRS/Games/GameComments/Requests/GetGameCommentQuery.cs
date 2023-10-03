using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Requests
{
    public sealed record class GetGameCommentQuery(Guid GameId): IRequest<List<GameHasComments>>;
}
