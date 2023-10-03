using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Requests.GetById
{
    public sealed record class GetGameCommentByIdQuery(Guid CommentId): IRequest<GameHasComments>;
}
