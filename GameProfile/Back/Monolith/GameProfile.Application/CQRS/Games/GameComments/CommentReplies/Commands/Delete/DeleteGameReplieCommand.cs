using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Delete
{
    public sealed record class DeleteGameReplieCommand(Guid ReplieId) : IRequest;
}
