using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHours;
using GameProfile.Application.CQRS.Profiles.ProfilesHasGames.Requests.GetProfileHasGamesTotalHoursVerification;
using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using GameProfile.Domain.Enums.Profile;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Reflection;

namespace GameProfile.Application.CQRS.Profiles.Requests.GetComp
{
    public sealed class GetProfileCompQueryHandler : IRequestHandler<GetProfileCompQuery, ProfileDTO>
    {
        private readonly IDatabaseContext _context;

        public GetProfileCompQueryHandler(IDatabaseContext context, ICacheService cacheService)
        {
            _context = context;
        }

        public async Task<ProfileDTO> Handle(GetProfileCompQuery request, CancellationToken cancellationToken)
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

            if (request.Verification == "yes")
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

            var profileHasGame = query
        .Where(p => p.ProfileId == request.ProfileId)
        .Join(_context.Games, phg => phg.GameId, g => g.Id, (phg, g) => new ProfileGamesDTO(
            g.Id,
            g.Title,
            g.HeaderImage,
            phg.MinutesInGame / 60,
            phg.StatusGame,
            phg.MinutesInGameVerified / 60
        )).ToList();

            var profile = await _context.Profiles.AsNoTracking().Where(x => x.Id == request.ProfileId).FirstOrDefaultAsync(cancellationToken);

            int hoursForSort = 0;
            if (request.Verification== "yes")
            {
                var query1 = _context.ProfileHasGames.AsNoTracking().AsQueryable();
                if (request.Filter == "1")
                {
                    query1 = query1.Where(x => x.StatusGame == StatusGameProgressions.Playing);
                }
                if (request.Filter == "2")
                {
                    query1 = query1.Where(x => x.StatusGame == StatusGameProgressions.Completed);
                }
                if (request.Filter == "3")
                {
                    query1 = query1.Where(x => x.StatusGame == StatusGameProgressions.Dropped);
                }
                if (request.Filter == "4")
                {
                    query1 = query1.Where(x => x.StatusGame == StatusGameProgressions.Planned);
                }

                hoursForSort = await query1.Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGameVerified) / 60;
            }
            else
            {
                var query2 = _context.ProfileHasGames.AsNoTracking().AsQueryable();
                if (request.Filter == "1")
                {
                    query2 = query2.Where(x => x.StatusGame == StatusGameProgressions.Playing);
                }
                if (request.Filter == "2")
                {
                    query2 = query2.Where(x => x.StatusGame == StatusGameProgressions.Completed);
                }
                if (request.Filter == "3")
                {
                    query2 = query2.Where(x => x.StatusGame == StatusGameProgressions.Dropped);
                }
                if (request.Filter == "4")
                {
                    query2= query2.Where(x => x.StatusGame == StatusGameProgressions.Planned);
                }

                hoursForSort = await query2.Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGame) / 60;
            }

            var totalHours = await _context.ProfileHasGames.AsNoTracking().Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGame + x.MinutesInGameVerified, cancellationToken) / 60;
            var totalHoursVerified = await _context.ProfileHasGames.AsNoTracking().Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGameVerified, cancellationToken) / 60;
            var totalHoursNotVerified = await _context.ProfileHasGames.AsNoTracking().Where(x => x.ProfileId == request.ProfileId).SumAsync(x => x.MinutesInGame, cancellationToken) / 60;

            ProfileDTO profileDTO = new () 
            { 
                NickName = profile.Name.Value,
                Description = profile.Description.Value,
                GameList = profileHasGame,
                TotalHoursForSort = hoursForSort,
                TotalHours = totalHours,
                TotalHoursNotVerification = totalHoursNotVerified,
                TotalHoursVerification = totalHoursVerified
            };

            return profileDTO;
        }
    }
}
