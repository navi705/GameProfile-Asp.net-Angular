using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Update
{
    public sealed record class UpdateGameCommentCommand(Guid CommentId, Guid ProfileId, string Comment) : IRequest<Result<GameHasComments>>;
}
