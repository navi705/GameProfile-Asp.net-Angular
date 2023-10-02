using MediatR;

namespace GameProfile.Application.CQRS.Games.Tags.Commands.AddTag
{
    public sealed record class AddTagCommand(string Tag) : IRequest;
}
