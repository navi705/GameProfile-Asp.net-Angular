using GameProfile.Application.CQRS.Games.Commands.DeleteGame;
using GameProfile.Application.CQRS.Games.Commands.UpdateGame;
using GameProfile.Application.CQRS.Games.Commands.UpdateGameReviews;
using GameProfile.Application.CQRS.Games.GameComments.Commands.Create;
using GameProfile.Application.CQRS.Games.GameComments.Commands.Delete;
using GameProfile.Application.CQRS.Games.GameComments.Commands.Update;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Create;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Delete;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Update;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Requests.GetById;
using GameProfile.Application.CQRS.Games.GameComments.Requests;
using GameProfile.Application.CQRS.Games.GameComments.Requests.GetById;
using GameProfile.Application.CQRS.Games.GameRating.Commands.Create;
using GameProfile.Application.CQRS.Games.GameRating.Commands.Delete;
using GameProfile.Application.CQRS.Games.GameRating.Commands.Update;
using GameProfile.Application.CQRS.Games.GameRating.Requests.GetAllByGameId;
using GameProfile.Application.CQRS.Games.GameRating.Requests.GetById;
using GameProfile.Application.CQRS.Games.Genres.Command.AddGenre;
using GameProfile.Application.CQRS.Games.Requests.GetGameById;
using GameProfile.Application.CQRS.Games.Requests.GetGames;
using GameProfile.Application.CQRS.Games.Requests.GetGamesByPubOrDev;
using GameProfile.Application.CQRS.Games.Requests.GetGamesBySearch;
using GameProfile.Application.CQRS.Games.Requests.GetGenres;
using GameProfile.Application.CQRS.Games.Requests.GetTags;
using GameProfile.Application.CQRS.Games.Tags.Commands.AddTag;
using GameProfile.Domain.Entities.GameEntites;
using GameProfile.Domain.Enums.Profile;
using GameProfile.Domain.ValueObjects.Game;
using GameProfile.WebAPI.Models;
using GameProfile.WebAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace GameProfile.WebAPI.Controllers
{
    [Route("game")]
    public sealed class GameController : ApiController
    {
        public GameController(ISender sender)
            : base(sender)
        {
        }
        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameById(Guid gameId)
        {
            var query = new GetGameByIdQuery(gameId);
            var response = await Sender.Send(query);
            return Ok(response);
        }
        [HttpGet("games/search")]
        public async Task<IActionResult> GetGamesBySearch(string title)
        {
            var query = new GetGamesBySearch(title);
            return Ok(await Sender.Send(query));
        }
        [HttpGet("games/devorpub")]
        public async Task<IActionResult> GetGamesByDevOrPub(string type, string who)
        {
            var query = new GetGamesByPubOrDevQuery(type, who);
            return Ok(await Sender.Send(query));
        }

        [HttpGet("games")]
        public async Task<IActionResult> GetGames([FromQuery] GetGamesBySortFiltersModel filters)
        {
            List<string> genres = new();
            List<string> genres1 = new();
            List<string> tags = new();
            List<string> tagsExcluding = new();
            List<StatusGameProgressions> statusGame = new();
            List<StatusGameProgressions> statusGameExcluding = new();
            if (filters.Genres is not null)
            {
                genres = filters.Genres[0].Split(',').ToList();
            }
            if (filters.GenresExcluding is not null)
            {
                genres1 = filters.GenresExcluding[0].Split(',').ToList();
            }
            if (filters.Tags is not null)
            {
                tags = filters.Tags[0].Split(',').ToList();
            }
            if (filters.TagsExcluding is not null)
            {
                tagsExcluding = filters.TagsExcluding[0].Split(',').ToList();
            }
            if (filters.StatusGameProgressions is not null && filters.StatusGameProgressions != "")
            {
                statusGame = filters.StatusGameProgressions.Split(',').Select(x => (StatusGameProgressions)Enum.Parse(typeof(StatusGameProgressions), x.Trim())).ToList();
            }
            if (filters.StatusGameProgressionsExcluding is not null && filters.StatusGameProgressionsExcluding != "")
            {
                statusGameExcluding = filters.StatusGameProgressionsExcluding.Split(',').Select(x => (StatusGameProgressions)Enum.Parse(typeof(StatusGameProgressions), x.Trim())).ToList();
            }

            Guid profileId = Guid.Empty;

            if (HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name") is not null)
            {
                profileId = new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
            }

            var query = new GetGamesQuery(filters.Sorting, filters.Page, filters.Nsfw, filters.ReleaseDateOf, filters.ReleaseDateTo, genres, genres1, tags, tagsExcluding, statusGame,
                statusGameExcluding, filters.RateOf, filters.RateTo, profileId);
            return Ok(await Sender.Send(query));
        }

        [HttpGet("genres")]
        public async Task<IActionResult> GetGenres()
        {
            var query = new GetGenresQuery();
            return Ok(await Sender.Send(query));
        }

        //[Authorize]
        //[TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        //[HttpPut("genres")]
        //public async Task<IActionResult> AddGenres(string genre)
        //{
        //    var query = new AddGenreCommand(genre);
        //    await Sender.Send(query);
        //    return Ok();
        //}

        ////[Authorize]
        ////[TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        ////[HttpDelete("genres")]
        ////public async Task<IActionResult> DeleteGenres(string genre)
        ////{
        ////    var query = new AddGenreCommand(genre);
        ////    await Sender.Send(query);
        ////    return Ok();
        ////}


        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            var query = new GetTagsQuery();
            return Ok(await Sender.Send(query));
        }

        //[Authorize]
        //[TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        //[HttpPut("tags")]
        //public async Task<IActionResult> AddTags(string tag)
        //{
        //    var query = new AddTagCommand(tag);
        //    await Sender.Send(query);

        //    return Ok();
        //}

        //[HttpPut]
        //public async Task<IActionResult> PutGame(Game game)
        //{
        //    var query = new CreateGameCommand(game.Title,
        //                          game.ReleaseDate,
        //                          game.HeaderImage,
        //                          game.Nsfw,
        //                          game.Description,
        //                          game.Genres,
        //                          game.Publishers,
        //                          game.Developers,
        //                          game.Screenshots,
        //                          game.ShopsLinkBuyGame,
        //                          game.AchievementsCount);
        //    await Sender.Send(query);
        //    return Ok();
        //}

        [HttpDelete]
        public async Task<IActionResult> DeleteGame(Guid gameId)
        {
            var query = new DeleteGameCommand(gameId);
            await Sender.Send(query);
            return Ok();
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateGame(Game game, Guid id)
        {
            var query = new UpdateGameCommand(game, id);
            await Sender.Send(query);
            return Ok();
        }

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
            var query = new GetGameHaveRatingFromProfileQuery(gameId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
            var gameRating = await Sender.Send(query);
            if (gameRating is null)
            {
                if (score == 0)
                {
                    return Ok();
                }
                var command = new CreateGameHaveRatingFromProfileCommand(gameId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), score);
                await Sender.Send(command);
            }
            else
            {
                if (score == 0)
                {
                    var query2 = new DeleteGameHaveRatingFromProfileCommand(gameId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value));
                    await Sender.Send(query2);

                    var gameHasRating1 = await Sender.Send(new GetGameHaveRatingFromProfileByGameIdQuery(gameId));
                    decimal avarageRating1 = 0;
                    if (gameHasRating1.Count > 0)
                    {
                        avarageRating1 = gameHasRating1.Sum(x => x.ReviewScore) / gameHasRating1.Count;

                    }
                    Review review1 = new(Domain.Enums.Game.SiteReviews.GameProfile, avarageRating1);

                    var command2 = new UpdateGameReviewsCommand(gameId, review1);
                    await Sender.Send(command2);

                    return Ok();
                }
                var command = new UpdateGameHaveRatingFromProfileCommand(gameId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), score);
                await Sender.Send(command);
            }

            var gameHasRating = await Sender.Send(new GetGameHaveRatingFromProfileByGameIdQuery(gameId));

            decimal avarageRating = gameHasRating.Sum(x => x.ReviewScore) / gameHasRating.Count;

            Review review = new(Domain.Enums.Game.SiteReviews.GameProfile, avarageRating);

            var command1 = new UpdateGameReviewsCommand(gameId, review);

            await Sender.Send(command1);

            return Ok();
        }

        [HttpGet("comments")]
        public async Task<IActionResult> GetGameComments(Guid gameId)
        {
            var query = new GetGameCommentQuery(gameId);
            var comments = await Sender.Send(query);
            return Ok(comments);
        }


        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("comments")]
        public async Task<IActionResult> PutGameComment(Guid gameId, string comment)
        {
            var query = new CreateGameCommentCommand(gameId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), comment);
            await Sender.Send(query);
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
                if(gameComment.ProfileId != new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value))
                {
                    return Forbid();
                }
            }

            var query = new UpdateGameCommentCommand(commentId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), comment);
            await Sender.Send(query);
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
            return Ok();
        }


        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("replie")]
        public async Task<IActionResult> PutGameReplie(Guid commentId, string replie)
        {
            var query = new CreateGameReplieCommand(commentId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), replie);
            await Sender.Send(query);
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
        public async Task<IActionResult> UpdateGameReplie(Guid replieId,string replie)
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

            var query = new UpdateGameReplieCommand(replieId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value),replie);
            await Sender.Send(query);
            return Ok();
        }


    }
}
