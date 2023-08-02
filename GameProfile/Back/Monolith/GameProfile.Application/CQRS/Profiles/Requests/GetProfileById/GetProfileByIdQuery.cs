using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetProfileById
{
    public sealed record class GetProfileByIdQuery (Guid Id) : IRequest<Profile>;
}
