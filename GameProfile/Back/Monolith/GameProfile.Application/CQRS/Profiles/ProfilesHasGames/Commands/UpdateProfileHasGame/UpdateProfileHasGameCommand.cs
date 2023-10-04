using GameProfile.Domain.Enums.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateProfileHasGame
{
    public sealed record class UpdateProfileHasGameCommand(Guid GameId,
                                                           int Hours,
                                                           StatusGameProgressions StatusGame,
                                                           Guid ProfileId,
                                                           int HoursInGameVerified) : IRequest;
}
