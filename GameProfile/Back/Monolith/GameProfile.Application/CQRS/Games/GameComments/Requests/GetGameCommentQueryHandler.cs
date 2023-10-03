using GameProfile.Application.Data;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Games.GameComments.Requests
{
    public sealed class GetGameCommentQueryHandler : IRequestHandler<GetGameCommentQuery,List<GameHasComments>>
    {
        private readonly IDatabaseContext _context;

        public GetGameCommentQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<GameHasComments>> Handle(GetGameCommentQuery request, CancellationToken cancellationToken)
        {
            var gameComment = await _context.GameHasComments.AsNoTracking().Include(g => g.Profile)
                .Include(h=> h.GameCommentHasReplies).ThenInclude(hh=> hh.Profile)
                .Where(comment => comment.GameId == request.GameId)
                .Select(x=> new GameHasComments(x.Id, x.ProfileId, x.GameId, x.CreatedDate, x.Comment) 
                { 
                    Profile = new Profile(x.ProfileId, x.Profile.Name, null, null) { GameHasRatingFromProfiles = x.Profile.GameHasRatingFromProfiles.Where(f=>f.ProfileId == x.ProfileId && f.GameId == x.GameId).ToList() }
                    , GameCommentHasReplies = x.GameCommentHasReplies.Select(h=> new GameCommentHasReplie(h.Id, h.ProfileId, h.CommentId, h.Created, h.Replie)
                    {
                        Profile = new Profile(h.ProfileId, h.Profile.Name, null, null) 
                    }).ToList()
                }
                )
                .ToListAsync(cancellationToken);
            return gameComment;
        }
    }
}
