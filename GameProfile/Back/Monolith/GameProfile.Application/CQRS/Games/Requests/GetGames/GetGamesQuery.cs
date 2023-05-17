using GameProfile.Domain.Entities;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.Requests
{
    public sealed record class GetGamesQuery (
        string sort,
        DateTime releaseDateOf,
        DateTime releaseDateTo) : IRequest<List<Game>>;
}
