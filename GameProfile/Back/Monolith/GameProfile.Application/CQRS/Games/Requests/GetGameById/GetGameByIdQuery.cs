﻿using GameProfile.Domain.Entities.GameEntites;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Requests.GetGameById;

public sealed record class GetGameByIdQuery(Guid GameId) : IRequest<Game?>;
