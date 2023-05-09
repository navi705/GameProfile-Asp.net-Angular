using GameProfile.Domain.Entities;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.UpdateGame
{
    public sealed record class UpdateGameCommand(Game game,Guid id) : IRequest;

}
