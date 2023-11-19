using GameProfile.Domain.Entities.Forum;
using GameProfile.Domain.Shared;
using MediatR;

namespace GameProfile.Application.CQRS.Forum.Commands.Create
{
    public sealed record class CreatePostCommand(string Title,
                                                 string Description,
                                                 string Topic,
                                                 Guid AuthorProfileId,
                                                 ICollection<String> Languages,
                                                 ICollection<String> Games) : IRequest<Result<Post>>;
}
