using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGameBySteamId
{
    public sealed record class GetProfileHasGameBySteamIdQuery(Guid ProfileId, int SteamId) : IRequest<ProfileHasGames>;
}
