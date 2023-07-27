using MediatR;

namespace GameProfile.Application.CQRS.Games.NotSteamGameAppID.Command.Create
{
    public sealed record class NotSteamGameAppIDCreateCommand(int AppId) : IRequest;
}
