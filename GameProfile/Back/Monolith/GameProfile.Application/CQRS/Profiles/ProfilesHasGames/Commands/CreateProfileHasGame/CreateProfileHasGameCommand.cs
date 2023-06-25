using GameProfile.Domain.Enums.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame
{
    public sealed record class CreateProfileHasGameCommand(Guid ProfileId,
                                                             Guid GameId,
                                                             StatusGameProgressions StatusGameProgressions,
                                                             int MinutesInGame) : IRequest;
}
