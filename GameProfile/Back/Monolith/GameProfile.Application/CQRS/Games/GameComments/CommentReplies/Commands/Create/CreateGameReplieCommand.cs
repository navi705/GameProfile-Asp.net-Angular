using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Create
{
    public sealed record class CreateGameReplieCommand(Guid CommentId, Guid ProfileId, string Comment) : IRequest<Result<GameCommentHasReplie>>;
}
