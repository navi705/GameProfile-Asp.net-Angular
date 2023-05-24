using GameProfile.Domain.Entities.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetProfileById
{
    public sealed record class GetProfileByIdQuery (Guid id) : IRequest<Profile>;
}
