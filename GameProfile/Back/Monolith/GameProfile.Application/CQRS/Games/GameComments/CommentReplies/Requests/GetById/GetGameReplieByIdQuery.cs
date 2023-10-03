using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Requests.GetById
{
    public sealed record class GetGameReplieByIdQuery(Guid Replie) : IRequest<GameCommentHasReplie>;
}
