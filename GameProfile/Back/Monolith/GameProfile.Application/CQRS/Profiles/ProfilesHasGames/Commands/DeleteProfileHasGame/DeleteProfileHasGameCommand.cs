using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.DeleteProfileHasGame
{
    public sealed record class DeleteProfileHasGameCommand(Guid gameId, Guid userId) : IRequest;
}
