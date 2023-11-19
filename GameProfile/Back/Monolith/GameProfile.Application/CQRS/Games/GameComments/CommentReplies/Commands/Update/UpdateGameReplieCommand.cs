using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Update
{
    public sealed record class UpdateGameReplieCommand(Guid ReplieId, Guid ProfileId, string Replie) : IRequest<Result<GameCommentHasReplie>>;
}
