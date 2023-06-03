using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats.GetCount
{
    public sealed record class GetCountProfilesQuery():IRequest<int>;
}
