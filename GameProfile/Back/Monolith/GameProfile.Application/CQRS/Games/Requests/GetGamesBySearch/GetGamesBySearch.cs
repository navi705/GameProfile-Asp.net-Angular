﻿using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGamesBySort
{
    public sealed record class GetGamesBySearch(string searchString) :IRequest<List<Game>>;
}
