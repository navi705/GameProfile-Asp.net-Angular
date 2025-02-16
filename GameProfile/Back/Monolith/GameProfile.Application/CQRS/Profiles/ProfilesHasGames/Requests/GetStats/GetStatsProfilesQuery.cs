﻿using GameProfile.Application.DTO;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetStats
{
    public sealed record class GetStatsProfilesQuery() : IRequest<List<StatsDTO>>;

}
