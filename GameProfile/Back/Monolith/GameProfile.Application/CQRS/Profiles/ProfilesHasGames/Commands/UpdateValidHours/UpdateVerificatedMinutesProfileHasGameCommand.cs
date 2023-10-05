using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateValidHours
{
    public sealed record class UpdateVerificatedMinutesProfileHasGameCommand(Guid ProfileId, Guid GameId, int Minutes) : IRequest;
}
