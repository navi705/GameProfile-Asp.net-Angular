using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetTotalHoursForProfile
{
    public sealed record class GetProfileHasGamesTotalHoursForProfileQuery(Guid ProfileId): IRequest<List<int>>;
}
