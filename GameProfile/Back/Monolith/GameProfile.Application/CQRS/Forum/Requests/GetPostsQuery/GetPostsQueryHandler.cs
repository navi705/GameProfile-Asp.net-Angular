using GameProfile.Application.Data;
using GameProfile.Application.DTO;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Entities.ProfileEntites;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameProfile.Application.CQRS.Forum.Requests.GetPostsQuery
{
    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, List<PostsSearchDTO>>
    {
        private readonly IDatabaseContext _context;

        public GetPostsQueryHandler(IDatabaseContext context)
        {
            _context = context;
        }

        public async Task<List<PostsSearchDTO>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Posts.AsNoTracking().AsQueryable();

            if (request.Sorting == "dateCreateDescending")
            {
                query = query.OrderByDescending(p => p.Created);
            }

            else if (request.Sorting == "dateCreateAscending")
            {
                query = query.OrderBy(p => p.Created);
            }

            else if (request.Sorting == "countRating")
            {
                query = query.OrderByDescending(p => p.Rating);
            }

            else if (request.Sorting == "countReplies")
            {
                query = query.OrderByDescending(p => p.MessagePosts.Count);
            }

            else if (request.Sorting == "lastReplies")
            {
                query = query.OrderByDescending(p => p.Updated);
            }

            if (request.Closed != "" && request.Closed is not null)
            {
                if (request.Closed == "yes")
                {
                    query = query.Where(x => x.Closed == true);
                }
                else
                {
                    query = query.Where(x => x.Closed == false);
                }
            }

            if (request.LanguageExcluding.Count > 0)
            {
                query = query.Where(g => g.Languages.Count(gg => request.LanguageExcluding.Contains(gg.StringFor)) == 0);
            }
            if (request.Language.Count > 0)
            {
                query = query.Where(g => g.Languages.Count(gg => request.Language.Contains(gg.StringFor)) == request.Language.Count());
            }


            if (request.Game.Count > 0)
            {
                query = query.Where(g => g.Games.Count(gg => request.Game.Contains(gg.Id.ToString())) == request.Game.Count());
            }
            if (request.GameExcluding.Count > 0)
            {
                query = query.Where(g => g.Games.Count(gg => request.GameExcluding.Contains(gg.Title)) == 0);
            }

            if (request.TopicExcluding.Count > 0)
            {
                query = query.Where(g => !g.Topic.Any(p => request.TopicExcluding.Contains(p.ToString())));
            }
            if (request.Topic.Count > 0)
            {
                query = query.Where(g => g.Topic.Any(p => request.Topic.Contains(p.ToString())));
            }

            if (request.RateOf is not null && request.RateTo is not null)
            {
                query = query.Where(x => x.Rating >= request.RateOf && x.Rating <= request.RateTo);
            }
            else if (request.RateOf is not null && request.RateTo is null)
            {
                query = query.Where(x => x.Rating >= request.RateOf);
            }
            else if (request.RateOf is null && request.RateTo is not null)
            {
                query = query.Where(x => x.Rating <= request.RateTo);
            }

            if (request.ReleaseDateOf != DateTime.MinValue && request.ReleaseDateTo != DateTime.MinValue)
            {
                query = query.Where(x => x.Created >= request.ReleaseDateOf && x.Created <= request.ReleaseDateTo.AddHours(23).AddMinutes(59).AddSeconds(59));
            }
            else if (request.ReleaseDateOf == DateTime.MinValue && request.ReleaseDateTo != DateTime.MinValue)
            {
                query = query.Where(x => x.Created <= request.ReleaseDateTo.AddHours(23).AddMinutes(59).AddSeconds(59));
            }
            else if (request.ReleaseDateOf != DateTime.MinValue && request.ReleaseDateTo == DateTime.MinValue)
            {
                query = query.Where(x => x.Created >= request.ReleaseDateOf.Date);
            }

            if (request.SeacrchString is not null && request.SeacrchString != "")
            {
                query = query.Where(x => EF.Functions.Like(x.Title, $"%{request.SeacrchString}%") || EF.Functions.Like(x.Description, $"%{request.SeacrchString}%"));
            }

            int skipPosts = request.Page * 50;
            query = query.Skip(skipPosts).Take(50);

            return await query.Include(p => p.Games).Include(p => p.Profile)
                           .Select(x => new PostsSearchDTO(x.Id, x.Title, x.Description, x.Topic, x.Created, x.Languages.Select(xa => xa.StringFor).ToList(),
                           x.Games.Select(gp => new PostGameDTO(gp.Id, gp.Title)).ToList()
                           , x.Closed, new PostAuthorDTO(x.Profile.Id, x.Profile.Name.Value), x.Rating)
                           ).ToListAsync();
        }
    }
}
