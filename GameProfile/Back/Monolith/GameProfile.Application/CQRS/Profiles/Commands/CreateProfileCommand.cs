using MediatR;

namespace GameProfile.Application.CQRS.Profiles.Commands
{
    public sealed record class CreateProfileCommand(string name,string description,string steamId) : IRequest;
}
