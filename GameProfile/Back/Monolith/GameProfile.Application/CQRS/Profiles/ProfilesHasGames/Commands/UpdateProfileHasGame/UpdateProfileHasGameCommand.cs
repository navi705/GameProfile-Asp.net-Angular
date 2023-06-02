using GameProfile.Domain.Enums.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.UpdateProfileHasGame
{
    public sealed record class UpdateProfileHasGameCommand(Guid gameId,int hours, StatusGameProgressions StatusGame,Guid profileId) : IRequest;
}
