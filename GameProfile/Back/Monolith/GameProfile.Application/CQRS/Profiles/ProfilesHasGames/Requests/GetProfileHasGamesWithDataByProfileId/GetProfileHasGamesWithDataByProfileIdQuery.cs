using GameProfile.Domain.AggregateRoots.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesWithDataByProfileId
{
    public sealed record class GetProfileHasGamesWithDataByProfileIdQuery(Guid ProfileId,string Filter,string Sort,string Verefication) : IRequest<List<AggregateProfileHasGame>>;
}
