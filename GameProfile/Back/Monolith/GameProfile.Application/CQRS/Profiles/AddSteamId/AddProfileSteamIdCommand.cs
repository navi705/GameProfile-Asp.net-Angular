using MediatR;

namespace GameProfile.Application.CQRS.Profiles.AddSteamId
{
    public sealed record class AddProfileSteamIdCommand(Guid ProfileId, string SteamId):IRequest;

}
