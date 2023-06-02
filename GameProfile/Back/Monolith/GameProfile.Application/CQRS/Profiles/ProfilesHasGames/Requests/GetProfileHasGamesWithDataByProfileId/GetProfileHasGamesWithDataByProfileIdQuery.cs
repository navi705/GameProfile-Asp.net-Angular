using GameProfile.Domain.AggregateRoots.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesWithDataByProfileId
{
    public sealed record class GetProfileHasGamesWithDataByProfileIdQuery(Guid profileId,string sort) : IRequest<List<AggregateProfileHasGame>>;
}
