using MediatR;

namespace GameProfile.Application.CQRS.Games.GameComments.Commands.Create
{
    public sealed record class CreateGameCommentCommand(Guid GameId, Guid ProfileId, string Comment) : IRequest;
}
