using GameProfile.Domain.Entities.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGameByProfileId
{
    public sealed record class GetProfileHasGameByProfileIdQuery(Guid ProifleId) : IRequest<List<ProfileHasGames>>;

}
