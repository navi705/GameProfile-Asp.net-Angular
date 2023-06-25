using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Commands
{
    public sealed record class CreateProfileCommand(string Name,string Description,string SteamId) : IRequest;
}
