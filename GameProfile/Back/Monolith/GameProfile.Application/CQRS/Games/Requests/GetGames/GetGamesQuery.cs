using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.Requests
{
    public sealed record class GetGamesQuery (
        string Sort,
        int Page,
        string Nsfw,
        DateTime ReleaseDateOf,
        DateTime ReleaseDateTo,
        List<string> Genres,
        List<string> GenresExcluding,
        List<string> Tags,
        List<string> TagsExcluding) : IRequest<List<Game>>;
}
