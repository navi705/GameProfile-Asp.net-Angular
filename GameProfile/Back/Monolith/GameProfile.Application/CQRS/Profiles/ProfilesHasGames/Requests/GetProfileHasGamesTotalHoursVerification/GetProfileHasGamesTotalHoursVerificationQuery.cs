using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHoursVerification
{
    public sealed record class GetProfileHasGamesTotalHoursVerificationQuery(Guid ProfileId, string Filter) : IRequest<int>;
}
