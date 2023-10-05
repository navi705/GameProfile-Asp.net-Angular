using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileSteamIds
{
    public sealed record class GetProfileSteamIdsQuery(Guid ProfileId) : IRequest<List<string>>;
}
