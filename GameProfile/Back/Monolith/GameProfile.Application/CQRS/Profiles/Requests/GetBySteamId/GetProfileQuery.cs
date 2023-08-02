using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetBySteamId
{
    public sealed record class GetProfileQuery (string SteamId) : IRequest<Profile?>;
}
