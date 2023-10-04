using GameProfile.Application.Data;
using GameProfile.Domain.AggregateRoots.Profile;
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
            var query = _context.ProfileHasGames.AsNoTracking().AsQueryable();

            if (request.Filter == "1")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Playing);
            }
            if (request.Filter == "2")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Completed);
            }
            if (request.Filter == "3")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Dropped);
            }
            if (request.Filter == "4")
            {
                query = query.Where(x => x.StatusGame == StatusGameProgressions.Planned);
            }

            if (request.Sort == "titleAtoZ")
            {
                query = query.OrderBy(x => x.Game.Title);
            }
            if (request.Sort == "titleZtoA")
            {
                query = query.OrderByDescending(x => x.Game.Title);
            }

            if (request.Verefication == "yes")
            {
                if (request.Sort == "hoursAsc")
                {
                    query = query.OrderBy(x => x.MinutesInGameVerified);
                }
                if (request.Sort == "hoursDesc")
                {
                    query = query.OrderByDescending(x => x.MinutesInGameVerified);
                }
            }
            else
            {
                if (request.Sort == "hoursAsc")
                {
                    query = query.OrderBy(x => x.MinutesInGame);
                }
                if (request.Sort == "hoursDesc")
                {
                    query = query.OrderByDescending(x => x.MinutesInGame);
                }
            }

            var aggregateProfileHasGame = query
        .Where(p => p.ProfileId == request.ProfileId)
        .Join(_context.Games, phg => phg.GameId, g => g.Id, (phg, g) => new AggregateProfileHasGame
        (
            g.Id,
            g.Title,
            g.HeaderImage,
            phg.MinutesInGame / 60,
            phg.StatusGame,
            phg.MinutesInGameVerified / 60
        )).ToList();

            return Task.FromResult(aggregateProfileHasGame);
        }
    }
}
