using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Delete
{
    public sealed record class DeleteGameCommentCommand(Guid CommentId): IRequest;
}
