using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.Requests
{
    public sealed record class GetGamesQuery (
        string sort,
        int page,
        string nsfw,
        DateTime releaseDateOf,
        DateTime releaseDateTo,
        List<string> genres,
        List<string> genresExcluding) : IRequest<List<Game>>;
}
