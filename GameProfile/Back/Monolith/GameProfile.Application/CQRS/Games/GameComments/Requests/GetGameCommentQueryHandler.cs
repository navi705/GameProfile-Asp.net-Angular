using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GameProfile.Application.CQRS.Games.GameComments.Requests
{
    public sealed class GetGameCommentQueryHandler : IRequestHandler<GetGameCommentQuery, List<GamesComment>>
    {
        private readonly IDatabaseContext _context;

        public GetGameCommentQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<GamesComment>> Handle(GetGameCommentQuery request, CancellationToken cancellationToken)
        {
            var gameComment = await _context.GameHasComments.AsNoTracking().Include(g => g.Profile)
                .Include(h => h.GameCommentHasReplies).ThenInclude(hh => hh.Profile)
                .Where(comment => comment.GameId == request.GameId)
                .Select(x => new GamesComment(x.Id, x.Comment, x.CreatedDate, new GameCommentAuthorDTO(x.ProfileId, x.Profile.Name.Value,
                x.Profile.GameHasRatingFromProfiles.Where(f => f.ProfileId == x.ProfileId && f.GameId == x.GameId).Select(x => x.ReviewScore).FirstOrDefault()),
                x.GameCommentHasReplies.Select(h=> new GamesReplie(h.Id, h.Replie, h.Created,new GameCommentAuthorDTO(h.ProfileId,h.Profile.Name.Value,
                h.Profile.GameHasRatingFromProfiles.Where(f => f.ProfileId == x.ProfileId && f.GameId == x.GameId).Select(x => x.ReviewScore).FirstOrDefault()
                ))).ToList()
                )
                )
                .ToListAsync(cancellationToken);
            return gameComment;
        }
    }
}
