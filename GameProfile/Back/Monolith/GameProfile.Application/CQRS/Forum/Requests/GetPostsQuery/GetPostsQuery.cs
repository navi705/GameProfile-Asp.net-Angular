using GameProfile.Domain.Entities.Forum;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.Requests.GetPostsQuery
{
    public sealed record class GetPostsQuery(string Sorting, int Page, DateTime ReleaseDateOf, DateTime ReleaseDateTo, 
        List<string>? Language, List<string?> Game,decimal? RateOf, decimal? RateTo, string? Closed, List<string?> Topic,
        List<string>? LanguageExcluding, List<string>? GameExcluding, List<string>? TopicExcluding, string SeacrchString) : IRequest<List<Post>>;
}
