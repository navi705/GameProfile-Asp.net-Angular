using GameProfile.Application.Data;
using GameProfile.Domain.AggregateRoots.Profile;
using GameProfile.Domain.Entities.Profile;
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
            //       var aggregateProfileHasGame = _context.ProfileHasGames
            //.Include(p => p.Game)
            //.Where(p => p.ProfileId == request.profileId)
            //.Select(p => new AggregateProfileHasGame
            //(
            //    Id = p.Game.Id,
            //    Title = p.Game.Title,
            //    HeaderImage = p.Game.HeaderImage,
            //    Hours = p.MinutesInGame / 60,
            //    StatusGame = p.StatusGame
            //))
            //.ToList();
            return null;
        }
    }
}
