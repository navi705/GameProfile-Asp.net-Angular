using MediatR;

namespace GameProfile.Application.CQRS.Games.NotSteamGameAppID.Requests
{
    public sealed record class NotSteamGameAppIdQuery(int AppId) : IRequest<bool>;
}
