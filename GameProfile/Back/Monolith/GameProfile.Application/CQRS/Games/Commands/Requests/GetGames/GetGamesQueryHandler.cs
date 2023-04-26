﻿using GameProfile.Application.Data;
using GameProfile.Domain.Entities;
using MediatR;

namespace GameProfile.Application.CQRS.Games.Commands.Requests.GetGames
{
    public sealed class GetGamesQueryHandler : IRequestHandler<GetGamesQuery,List<Game>>
    {
        private readonly IDatabaseContext _context;
        public GetGamesQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public Task<List<Game>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
        {
            var games =  _context.Games.ToList();
            return Task.FromResult(games);
        }
    }
}
