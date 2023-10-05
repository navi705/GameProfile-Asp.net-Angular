using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetSteamIdBySteamId
{
    public sealed record class GetSteamIdBySteamIdQuery(string SteamId) : IRequest<bool>;
}
