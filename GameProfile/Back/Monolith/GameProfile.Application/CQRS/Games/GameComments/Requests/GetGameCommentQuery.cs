using GameProfile.Application.DTO;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Requests
{
    public sealed record class GetGameCommentQuery(Guid GameId): IRequest<List<GamesComment>>;
}
