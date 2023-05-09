﻿using GameProfile.Domain.Entities;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.Requests;

public sealed record class GetGameByIdQuery (Guid GameId) : IRequest<Game>;