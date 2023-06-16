﻿using GameProfile.Application.Data;
using GameProfile.Domain.Entities.Profile;
using MediatR;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Commands.CreateProfileHasGame
{
    public sealed class CreateProfileHasGameCommandHandler : IRequestHandler<CreateProfileHasGameCommand>
    {
        private readonly IDatabaseContext _context;
        public CreateProfileHasGameCommandHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateProfileHasGameCommand request, CancellationToken cancellationToken)
        {
            var profileHasGame = new ProfileHasGames(Guid.Empty, request.profileId, request.gameId, request.statusGameProgressions, request.minutesInGame);
            _context.ProfileHasGames.Add(profileHasGame);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}