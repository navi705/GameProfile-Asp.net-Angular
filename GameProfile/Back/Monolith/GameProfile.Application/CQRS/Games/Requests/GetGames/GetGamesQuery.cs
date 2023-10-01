using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Enums.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGames
{
    public sealed record class GetGamesQuery(
        string Sort,
        int Page,
        string Nsfw,
        DateTime ReleaseDateOf,
        DateTime ReleaseDateTo,
        List<string> Genres,
        List<string> GenresExcluding,
        List<string> Tags,
        List<string> TagsExcluding,
        List<StatusGameProgressions> StatusGame,
        List<StatusGameProgressions> StatusGameExcluding,
        decimal? RateOf,
        decimal? RateTo,
        Guid ProfileId) : IRequest<List<Game>>;
}
