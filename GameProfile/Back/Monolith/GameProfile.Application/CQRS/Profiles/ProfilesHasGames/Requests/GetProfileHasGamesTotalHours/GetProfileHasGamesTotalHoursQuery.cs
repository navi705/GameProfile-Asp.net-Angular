using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHours
{
    public sealed record class GetProfileHasGamesTotalHoursQuery(Guid ProfieId,string Filter) :IRequest<int>;
}
