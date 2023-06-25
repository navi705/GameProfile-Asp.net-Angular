using GameProfile.Domain.Entities.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetBySteamId
{
    public sealed record class GetProfileQuery (string SteamId) : IRequest<Profile?>;
}
