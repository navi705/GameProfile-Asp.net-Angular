using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHours
{
    public sealed record class GetProfileHasGamesTotalHoursQuery(Guid profieId,string filter) :IRequest<int>;
}
