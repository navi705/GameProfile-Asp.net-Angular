using GameProfile.Domain.Enums.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame
{
    public sealed record class CreateProfileHasGameCommand(Guid profileId,
                                                             Guid gameId,
                                                             StatusGameProgressions statusGameProgressions,
                                                             int minutesInGame) : IRequest;
}
