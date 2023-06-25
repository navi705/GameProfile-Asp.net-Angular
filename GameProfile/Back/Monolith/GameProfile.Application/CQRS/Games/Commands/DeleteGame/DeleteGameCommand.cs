using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.DeleteGame
{
    public sealed record class DeleteGameCommand (Guid GameId) : IRequest;
}
