using MediatR;

namespace GameProfile.Application.CQRS.Games.GamesSteamAppId.Commands
{
    public sealed record class CreateGamesSteamAppIdQuery(Guid GameId, int SteamAppId) : IRequest;
}
