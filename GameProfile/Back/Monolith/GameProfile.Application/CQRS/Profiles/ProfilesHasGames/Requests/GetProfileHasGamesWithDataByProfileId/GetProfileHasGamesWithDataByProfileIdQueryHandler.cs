using GameProfile.Application.Data;
using GameProfile.Domain.AggregateRoots.Profile;
using GameProfile.Domain.Entities.Profile;
using GameProfile.Domain.Enums.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesWithDataByProfileId
{
    public sealed class GetProfileHasGamesWithDataByProfileIdQueryHandler : IRequestHandler<GetProfileHasGamesWithDataByProfileIdQuery, List<AggregateProfileHasGame>>
    {
        private readonly IDatabaseContext _context;
        public GetProfileHasGamesWithDataByProfileIdQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }
        public Task<List<AggregateProfileHasGame>> Handle(GetProfileHasGamesWithDataByProfileIdQuery request, CancellationToken cancellationToken)
        {
            var query = _context.ProfileHasGames.AsQueryable();

            if (request.filter == "1")
            {
               query= query.Where(x=>x.StatusGame == StatusGameProgressions.Playing);
            }
            if (request.filter == "2")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Completed);
            }
            if (request.filter == "3")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Dropped);
            }
            if (request.filter == "4")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Planned);
            }

            if(request.sort == "titleAtoZ")
            {
                query = query.OrderBy(x => x.Game.Title);
            }
            if (request.sort == "titleZtoA")
            {
                query = query.OrderByDescending(x => x.Game.Title);
            }
            if (request.sort == "hoursAsc")
            {
                query = query.OrderBy(x => x.MinutesInGame);
            }
            if (request.sort == "hoursDesc")
            {
                query = query.OrderByDescending(x => x.MinutesInGame);
            }

            var aggregateProfileHasGame = query
        .Where(p => p.ProfileId == request.profileId)
        .Join(_context.Games, phg => phg.GameId, g => g.Id, (phg, g) => new AggregateProfileHasGame
        (
            g.Id,
            g.Title,
            g.HeaderImage,
            phg.MinutesInGame / 60,
            phg.StatusGame
        ))
        .ToList();

            return Task.FromResult(aggregateProfileHasGame);
        }
    }
}
