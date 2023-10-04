using GameProfile.Application.CQRS.Forum.Commands.CloseOrOpen;
using GameProfile.Application.CQRS.Forum.Commands.Create;
using GameProfile.Application.CQRS.Forum.Commands.Delete;
using GameProfile.Application.CQRS.Forum.Commands.UpdateRating;
using GameProfile.Application.CQRS.Forum.Commands.UpdateTimeUpdate;
using GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Create;
using GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Delete;
using GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Commands.Update;
using GameProfile.Application.CQRS.Forum.PostHaveRatingFromProfile.Requests.GetByProfileIdPostId;
using GameProfile.Application.CQRS.Forum.PostMessage.Commands;
using GameProfile.Application.CQRS.Forum.PostMessage.Commands.Create;
using GameProfile.Application.CQRS.Forum.PostMessage.Commands.Delete;
using GameProfile.Application.CQRS.Forum.PostMessage.Commands.Update;
using GameProfile.Application.CQRS.Forum.PostMessage.Requests.GetById;
using GameProfile.Application.CQRS.Forum.Replie.Commands.Create;
using GameProfile.Application.CQRS.Forum.Replie.Commands.Delete;
using GameProfile.Application.CQRS.Forum.Replie.Commands.Update;
using GameProfile.Application.CQRS.Forum.Replie.Requests.GetById;
using GameProfile.Application.CQRS.Forum.Requests.GetPostById;
using GameProfile.Application.CQRS.Forum.Requests.GetPostsQuery;
using GameProfile.Application.CQRS.Profiles.Notification.Commands.Create;
using GameProfile.Domain.Entities.Forum;
using GameProfile.WebAPI.Models;
using GameProfile.WebAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    [Route("forum")]
    public sealed class ForumContoller : ApiController
    {
        public ForumContoller(ISender sender)
           : base(sender)
        {
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("add")]
        public async Task<IActionResult> PutPost(string title, string description, string topic, string? games, string languages)
        {
            List<string> games1 = new();
            List<string> languages1 = new();
            if (languages is not null && languages != "")
            {
                languages1 = languages.Split(',').ToList();
            }
            if (games is not null && games != "")
            {
                games1 = games.Split(',').ToList();
            }

            var query = new CreatePostCommand(title, description, topic, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), languages1, games1);
            await Sender.Send(query);
            return Ok();
        }

        [HttpGet()]
        public async Task<IActionResult> GetPost([FromQuery] GetPostsBySortFiltersModels getPostsBySortFilters)
        {
            List<string> languages = new();
            List<string> languagesExcluding = new();
            List<string> games = new();
            List<string> gamesExcluding = new();
            List<string> topic = new();
            List<string> topicExcluding = new();
            if (getPostsBySortFilters.Language is not null && getPostsBySortFilters.Language != "")
            {
                languages = getPostsBySortFilters.Language.Split(",").ToList();
            }
            if (getPostsBySortFilters.Game is not null && getPostsBySortFilters.Game != "")
            {
                games = getPostsBySortFilters.Game.Split(",").ToList();
            }
            if (getPostsBySortFilters.Topic is not null && getPostsBySortFilters.Topic != "")
            {
                topic = getPostsBySortFilters.Topic.Split(",").ToList();
            }
            if (getPostsBySortFilters.GameExcluding is not null && getPostsBySortFilters.GameExcluding != "")
            {
                gamesExcluding = getPostsBySortFilters.GameExcluding.Split(",").ToList();
            }
            if (getPostsBySortFilters.LanguageExcluding is not null && getPostsBySortFilters.LanguageExcluding != "")
            {
                languagesExcluding = getPostsBySortFilters.LanguageExcluding.Split(",").ToList();
            }
            if (getPostsBySortFilters.TopicExcluding is not null && getPostsBySortFilters.TopicExcluding != "")
            {
                topicExcluding = getPostsBySortFilters.TopicExcluding.Split(",").ToList();
            }

            var query = new GetPostsQuery(getPostsBySortFilters.Sorting, getPostsBySortFilters.Page, getPostsBySortFilters.CreatedDateOf
                , getPostsBySortFilters.CreatedDateTo, languages, games, getPostsBySortFilters.RateOf, getPostsBySortFilters.RateTo,
                getPostsBySortFilters.Closed, topic, languagesExcluding, gamesExcluding, topicExcluding, getPostsBySortFilters.SearchString);
            var answer = await Sender.Send(query);
            foreach (var post in answer)
            {
                if (post.Games is not null)
                {
                    foreach (var game in post.Games)
                    {
                        game.Posts = null;
                    }
                }
            }
            return Ok(answer);
        }
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetForumById(Guid postId)
        {
            var post = await Sender.Send(new GetPostByIdQuery(postId));

            if (post.Games is not null)
            {
                foreach (var game in post.Games)
                {
                    game.Posts = null;
                }
            }

            if (post.MessagePosts is not null)
            {
                foreach (var message in post.MessagePosts)
                {
                    message.Profile.Messages = null;
                }
            }

            return Ok(post);
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteForumById(Guid Id)
        {
            var post = await Sender.Send(new GetPostByIdQuery(Id));

            if (post.Games is not null)
            {
                foreach (var game in post.Games)
                {
                    game.Posts = null;
                }
            }

            if (post.Author.ToString() != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                return Forbid();
            }

            var query = new DeleteForumCommand(Id);
            await Sender.Send(query);
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPost()]
        public async Task<IActionResult> CloseOrOpenForumById(Guid id, bool close)
        {
            var post = await Sender.Send(new GetPostByIdQuery(id));

            if (post is null)
            {
                return Forbid();
            }

            if (post.Games is not null)
            {
                foreach (var game in post.Games)
                {
                    game.Posts = null;
                }
            }

            if (post.Author.ToString() != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                return Forbid();
            }

            var query = new CloseOrOpenForumCommand(id, close);
            await Sender.Send(query);
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPost("rating")]
        public async Task<IActionResult> PostAddRating(Guid postId, string rating)
        {
            var postHaveRating = await Sender.Send(new GetByProfilePostIdPostHaveRatingFromProfileQuery(postId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)));

            if (postHaveRating is null)
            {
                if (rating == "positive")
                {
                    var query = new CreatePostHaveRatingFromProfileCommand(postId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), true);
                    await Sender.Send(query);

                    await Sender.Send(new UpdateRatingPostCommand(postId, 1));
                }
                else
                {
                    var query = new CreatePostHaveRatingFromProfileCommand(postId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), false);
                    await Sender.Send(query);

                    await Sender.Send(new UpdateRatingPostCommand(postId, -1));
                }
            }
            else
            {

                if (rating == "positive")
                {
                    if (postHaveRating.IsPositive == false)
                    {
                        var query = new UpdatePostHaveRatingFromProfileCommand(postId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), true);
                        await Sender.Send(query);

                        await Sender.Send(new UpdateRatingPostCommand(postId, 2));
                    }
                    else
                    {
                        var query = new DeletePostHaveRatingFromProfileCommand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), postId);
                        await Sender.Send(query);

                        await Sender.Send(new UpdateRatingPostCommand(postId, -1));
                    }
                }
                else
                {
                    if (postHaveRating.IsPositive == true)
                    {
                        var query = new UpdatePostHaveRatingFromProfileCommand(postId, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), false);
                        await Sender.Send(query);

                        await Sender.Send(new UpdateRatingPostCommand(postId, -2));
                    }
                    else
                    {
                        var query = new DeletePostHaveRatingFromProfileCommand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), postId);
                        await Sender.Send(query);

                        await Sender.Send(new UpdateRatingPostCommand(postId, 1));
                    }
                }
            }


            return Ok();
        }

        // MessagePost

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("messagePost/add")]
        public async Task<IActionResult> CreateMessagePost(string content, Guid postId)
        {
            var query = new CreateMessagePostCommand(content, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), postId);
            await Sender.Send(query);

            // send owner notification
            var post = await Sender.Send(new GetPostByIdQuery(postId));

            if (post.Author != new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value))
            {
                var queryNotification = new CreateProfileNotificationComand(post.Author, $"MessagePost {post.Id}");
                await Sender.Send(queryNotification);
            }

            var queryUpdateTime = new UpdateTimeUpdateCommand(postId);
            await Sender.Send(queryUpdateTime);

            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("messagePost")]
        public async Task<IActionResult> DeleteMessagePost(Guid messagePostID)
        {
            var query1 = new GetPostMessageByIdQuery(messagePostID);

            var postMessage = await Sender.Send(query1);

            if (postMessage.AuthorId.ToString() != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                return Forbid();
            }

            var query = new DeleteMessagePostCommand(messagePostID);
            await Sender.Send(query);

            //var queryUpdateTime = new UpdateTimeUpdateCommand(postMessage.PostId);
            //await Sender.Send(queryUpdateTime);

            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("messagePost/update")]
        public async Task<IActionResult> UpdateMessagePost(Guid messagePostID, string content)
        {
            var query1 = new GetPostMessageByIdQuery(messagePostID);

            var postMessage = await Sender.Send(query1);

            if (postMessage.AuthorId.ToString() != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                return Forbid();
            }

            var query = new UpdateMessagePostCommand(messagePostID, content);
            await Sender.Send(query);

            //var queryUpdateTime = new UpdateTimeUpdateCommand(postMessage.PostId);
            //await Sender.Send(queryUpdateTime);

            return Ok();
        }

        //Replies

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("replie/add")]
        public async Task<IActionResult> CreateReplie(string content, Guid messagePostId)
        {
            var query = new CreateReplieCommand(content, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), messagePostId);
            var replie = await Sender.Send(query);

            var query1 = new GetPostMessageByIdQuery(messagePostId);

            var postMessage = await Sender.Send(query1);

            // send owner notification

            if (postMessage.AuthorId != new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value))
            {
                var queryNotification = new CreateProfileNotificationComand(postMessage.AuthorId, $"RepliePost {postMessage.PostId}");
                await Sender.Send(queryNotification);
            }

            var queryUpdateTime = new UpdateTimeUpdateCommand(postMessage.PostId);
            await Sender.Send(queryUpdateTime);

            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("replie")]
        public async Task<IActionResult> DeleteReplie(Guid replieId)
        {
            var replie = await Sender.Send(new GetRepileByIdQuery(replieId));
            if (replie.AuthorId.ToString() != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                return Forbid();
            }

            await Sender.Send(new DeleteReplieCommand(replieId));

            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("replie/update")]
        public async Task<IActionResult> UpdateReplie(Guid replieId, string content)
        {
            var replie1 = await Sender.Send(new GetRepileByIdQuery(replieId));
            if (replie1.AuthorId.ToString() != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                return Forbid();
            }

            var replie = await Sender.Send(new UpdateReplieCommand(replieId, content));

            //var queryUpdateTime = new UpdateTimeUpdateCommand(replie.MessagePost.PostId);
            //await Sender.Send(queryUpdateTime);

            return Ok();
        }

    }
}
