using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetTags
{
    public sealed record class GetTagsQuery() : IRequest<List<string>>;
}
