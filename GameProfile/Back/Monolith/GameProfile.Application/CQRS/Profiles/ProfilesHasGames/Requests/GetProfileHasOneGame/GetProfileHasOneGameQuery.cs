﻿using GameProfile.Domain.AggregateRoots.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasOneGame
{
    public sealed record class GetProfileHasOneGameQuery(Guid ProfileId, Guid Gameid) : IRequest<AggregateProfileHasGame> ;

}