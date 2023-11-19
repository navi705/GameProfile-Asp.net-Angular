using GameProfile.Application.DTO;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetComp
{
    public sealed record class GetProfileCompQuery(Guid ProfileId,string? Filter, string? Sort,string? Verification) : IRequest<ProfileDTO>;
}
