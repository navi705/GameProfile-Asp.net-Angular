using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGameByProfileId
{
    public sealed record class GetProfileHasGameByProfileIdQuery(Guid ProifleId) : IRequest<List<ProfileHasGames>>;

}
