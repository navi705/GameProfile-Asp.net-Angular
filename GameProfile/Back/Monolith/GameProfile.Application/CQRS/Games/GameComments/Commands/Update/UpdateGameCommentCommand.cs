using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Update
{
    public sealed record class UpdateGameCommentCommand(Guid CommentId, Guid ProfileId, string Comment) : IRequest;
}
