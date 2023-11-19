using MediatR;

namespace GameProfile.Application.CQRS.Forum.Requests.GetPostAuthor
{
    public sealed record class GetPostAuthorQuery(Guid Id) : IRequest<string>;
}
