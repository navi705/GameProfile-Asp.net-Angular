using MediatR;

namespace GameProfile.Application.CQRS.Forum.Replie.Requests.GetById
{
    public sealed record class GetRepileByIdQuery(Guid ReplieId) : IRequest<GameProfile.Domain.Entities.Forum.Replie>;
}
