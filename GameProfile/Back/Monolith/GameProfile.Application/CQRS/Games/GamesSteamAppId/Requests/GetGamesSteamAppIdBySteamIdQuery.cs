﻿using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.GamesSteamAppId.Requests
{
    public sealed record class GetGamesSteamAppIdBySteamIdQuery(int steamId):IRequest<GameSteamId>;
}