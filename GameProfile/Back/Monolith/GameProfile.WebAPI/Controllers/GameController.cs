using GameProfile.Application.CQRS.Games.GameComments.Commands.Create;
using GameProfile.Application.CQRS.Games.GameComments.Commands.Delete;
using GameProfile.Application.CQRS.Games.GameComments.Commands.Update;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Create;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Delete;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Update;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Requests.GetById;
using GameProfile.Application.CQRS.Games.GameComments.Requests;
using GameProfile.Application.CQRS.Games.GameComments.Requests.GetById;
using GameProfile.Application.CQRS.Games.GameRating.Commands.GameRatingComp;
using GameProfile.Application.CQRS.Games.GameRating.Requests.GetById;
using GameProfile.Application.CQRS.Games.Requests.GetGameById;
using GameProfile.Application.CQRS.Games.Requests.GetGames;
using GameProfile.Application.CQRS.Games.Requests.GetGamesByPubOrDev;
using GameProfile.Application.CQRS.Games.Requests.GetGamesBySearch;
using GameProfile.Application.CQRS.Games.Requests.GetGenres;
using GameProfile.Application.CQRS.Games.Requests.GetTags;
using GameProfile.Application.CQRS.Profiles.Notification.Commands.Create;
using GameProfile.Infrastructure.Steam;
using GameProfile.WebAPI.ApiCompilation;
using GameProfile.WebAPI.Models.ArgumentModels;
using GameProfile.WebAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    [Route("game")]
    public sealed class GameController : ApiController
    {
        private readonly ILogger<GameController> _logger;
        private readonly SteamApiCompilation _steamApiCompilation;
        public GameController(ISender sender, ILogger<GameController> logger, ISteamApi steamApi)
            : base(sender)
        {
            _logger = logger;
            _steamApiCompilation = new(sender, steamApi);
        }

        #region Games
        [AllowAnonymous]
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameById(Guid gameId)
        {
            var query = new GetGameByIdQuery(gameId);
            var response = await Sender.Send(query);
            _logger.LogInformation("Someone get game by id");
            return Ok(response);
        }
        [AllowAnonymous]
        [HttpGet("games/search")]
        public async Task<IActionResult> GetGamesBySearch(string title)
        {
            var query = new GetGamesBySearch(title);
            _logger.LogInformation($"Someone get games by search {title}");
            return Ok(await Sender.Send(query));
        }

        [AllowAnonymous]
        [HttpGet("games/devorpub")]
        public async Task<IActionResult> GetGamesByDevOrPub(string type, string who)
        {
            Guid profileId = Guid.Empty;

            if (HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") is not null)
            {
                profileId = new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
            }

            var query = new GetGamesByPubOrDevQuery(type, who, profileId);
            _logger.LogInformation($"Someone get games by DevOrPub {who}");
            return Ok(await Sender.Send(query));
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("add-game-from-steam")]
        public async Task<IActionResult> AddSteamGame([FromQuery]int appId)
        {
            var answer = await _steamApiCompilation.AddGame(appId);
            if (answer == Guid.Empty)
            {
                _logger.LogInformation($"Game alredy have or something went wrong {appId}");
                return BadRequest("Game alredy have or something went wrong ");
            }
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} add game from steam {appId}");
            return Ok(answer);
        }

        [AllowAnonymous]
        [HttpGet("games")]
        public async Task<IActionResult> GetGames([FromQuery] GetGamesBySortFiltersModel filters)
        {
            Guid profileId = Guid.Empty;

            if (HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") is not null)
            {
                profileId = new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
            }

            var query = new GetGamesQuery(filters.Sorting,
                                          filters.Page,
                                          filters.Nsfw,
                                          filters.ReleaseDateOf,
                                          filters.ReleaseDateTo,
                                          filters.Genres,
                                          filters.GenresExcluding,
                                          filters.Tags,
                                          filters.TagsExcluding,
                                          filters.StatusGameProgressions,
                                          filters.StatusGameProgressionsExcluding,
                                          filters.RateOf,
                                          filters.RateTo,
                                          profileId);
            _logger.LogInformation("Someone get games by filters");
            return Ok(await Sender.Send(query));
        }

        [AllowAnonymous]
        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            var query = new GetGenresQuery();
            _logger.LogInformation("Someone get genres");
            return Ok(await Sender.Send(query));
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            var query = new GetTagsQuery();
            _logger.LogInformation("Someone get tags");
            return Ok(await Sender.Send(query));
        }
        #endregion

        #region Review
        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet("review")]
        public async Task<IActionResult> GetGameProfileReview(Guid gameId)
        {
            var query = new GetGameHaveRatingFromProfileQuery(gameId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            var gameRating = await Sender.Send(query);

            if (gameRating is not null)
            {
                return Ok(gameRating.ReviewScore);
            }
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} Get review for game {gameId} rating {gameRating.ReviewScore}");
            return Ok(0);
        }
       
        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("review")]
        public async Task<IActionResult> PutGameProfileReview(Guid gameId, int score)
        {
            if (score < 0 && score > 10)
            {
                return BadRequest();
            }

            var command1 = new GameRatingCompCommand(
                gameId,
                new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value),
                score);

            await Sender.Send(command1);
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} Put review for game {gameId} rating {score}");
            return Ok();
        }

        #endregion

        #region Comments
        [AllowAnonymous]
        [HttpGet("comments")]
        public async Task<IActionResult> GetGameComments(Guid gameId)
        {
            var query = new GetGameCommentQuery(gameId);
            var comments = await Sender.Send(query);
            _logger.LogInformation($"Someone get game {gameId} comments");
            return Ok(comments);
        }


        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("comments")]
        public async Task<IActionResult> PutGameComment(Guid gameId, string comment)
        {
            var query = new CreateGameCommentCommand(gameId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), comment);
           var result= await Sender.Send(query);
            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return Forbid(result.ErrorMessage);
            }
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} add comment to game {gameId}");
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("comments/update")]
        public async Task<IActionResult> UpdateGameComment(Guid commentId, string comment)
        {
            var gameComment = await Sender.Send(new GetGameCommentByIdQuery(commentId));

            if (gameComment is null)
            {
                return BadRequest();
            }
            else
            {
                if (gameComment.ProfileId != new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value))
                {
                    return Forbid();
                }
            }

            var query = new UpdateGameCommentCommand(commentId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), comment);
            var result = await Sender.Send(query);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return Forbid(result.ErrorMessage);
            }
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} update content {commentId}");
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("comments")]
        public async Task<IActionResult> DeleteGameComment(Guid commentId)
        {
            var gameComment = await Sender.Send(new GetGameCommentByIdQuery(commentId));

            if (gameComment is null)
            {
                return BadRequest();
            }

            else
            {
                if (gameComment.ProfileId != new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value))
                {
                    return Forbid();
                }
            }

            var query = new DeleteGameCommentCommand(commentId);
            await Sender.Send(query);
            _logger.LogInformation($"User by id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} delete{commentId}");
            return Ok();
        }
        #endregion

        #region Replie
        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("replie")]
        public async Task<IActionResult> PutGameReplie(Guid commentId, string replie)
        {
            var query = new CreateGameReplieCommand(commentId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), replie);
            var result = await Sender.Send(query);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return Forbid(result.ErrorMessage);
            }

            var gameComment = await Sender.Send(new GetGameCommentByIdQuery(commentId));

            // send owner notification
            if (gameComment.ProfileId != new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value))
            {
                var queryNotification = new CreateProfileNotificationComand(gameComment.ProfileId, $"ReplieGame {gameComment.GameId}");
                await Sender.Send(queryNotification);
            }

            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("replie")]
        public async Task<IActionResult> DeleteGameReplie(Guid replieId)
        {
            var gameReplie = await Sender.Send(new GetGameReplieByIdQuery(replieId));

            if (gameReplie is null)
            {
                return BadRequest();
            }
            else
            {
                if (gameReplie.ProfileId != new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value))
                {
                    return Forbid();
                }
            }

            var query = new DeleteGameReplieCommand(replieId);
            await Sender.Send(query);
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("replie/update")]
        public async Task<IActionResult> UpdateGameReplie(Guid replieId, string replie)
        {
            var gameReplie = await Sender.Send(new GetGameReplieByIdQuery(replieId));

            if (gameReplie is null)
            {
                return BadRequest();
            }
            else
            {
                if (gameReplie.ProfileId != new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value))
                {
                    return Forbid();
                }
            }

            var query = new UpdateGameReplieCommand(replieId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), replie);
            var result = await Sender.Send(query);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                return Forbid(result.ErrorMessage);
            }

            return Ok();
        }
        #endregion
    }
}
