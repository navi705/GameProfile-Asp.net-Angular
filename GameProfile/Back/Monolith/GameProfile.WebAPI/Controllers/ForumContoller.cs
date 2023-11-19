using GameProfile.Application.CQRS.Forum.Commands.CloseOrOpen;
using GameProfile.Application.CQRS.Forum.Commands.Create;
using GameProfile.Application.CQRS.Forum.Commands.Delete;
using GameProfile.Application.CQRS.Forum.Commands.UpdateRatingComplitation;
using GameProfile.Application.CQRS.Forum.Commands.UpdateTimeUpdate;
using GameProfile.Application.CQRS.Forum.PostMessage.Commands.Create;
using GameProfile.Application.CQRS.Forum.PostMessage.Commands.Delete;
using GameProfile.Application.CQRS.Forum.PostMessage.Commands.Update;
using GameProfile.Application.CQRS.Forum.PostMessage.Requests.GetById;
using GameProfile.Application.CQRS.Forum.Replie.Commands.Create;
using GameProfile.Application.CQRS.Forum.Replie.Commands.Delete;
using GameProfile.Application.CQRS.Forum.Replie.Commands.Update;
using GameProfile.Application.CQRS.Forum.Replie.Requests.GetById;
using GameProfile.Application.CQRS.Forum.Requests.GetPostAuthor;
using GameProfile.Application.CQRS.Forum.Requests.GetPostById;
using GameProfile.Application.CQRS.Forum.Requests.GetPostsQuery;
using GameProfile.Application.CQRS.Profiles.Notification.Commands.Create;
using GameProfile.WebAPI.Models.ArgumentModels;
using GameProfile.WebAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GameProfile.WebAPI.Controllers
{
    [Route("forum")]
    public sealed class ForumContoller : ApiController
    {
        private readonly ILogger<ForumContoller> _logger;
        public ForumContoller(ISender sender, ILogger<ForumContoller> logger)
           : base(sender)
        {
            _logger = logger;
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("add")]
        public async Task<IActionResult> PutPost(string title, string description, string topic, [FromQuery]List<string>? games, [FromQuery][Required] List<string> languages)
        {
            if (games is null)
            {
                games = new List<string>();
            }

            var query = new CreatePostCommand(title,
                                              description,
                                              topic,
                                              new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value),
                                              languages,
                                              games);

            var queryResult = await Sender.Send(query);
            if (!string.IsNullOrWhiteSpace(queryResult.ErrorMessage))
            {
                _logger.LogWarning($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} " +
                    $"Create a new Post failed Error: {queryResult.ErrorMessage}");
                return Forbid(queryResult.ErrorMessage);
            }
            _logger.LogInformation($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} Create a new Post");
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> GetPost([FromQuery] GetPostsBySortFiltersModels getPostsBySortFilters)
        {
            var query = new GetPostsQuery(getPostsBySortFilters.Sorting,
                                          getPostsBySortFilters.Page,
                                          getPostsBySortFilters.CreatedDateOf,
                                          getPostsBySortFilters.CreatedDateTo,
                                          getPostsBySortFilters.Languages,
                                          getPostsBySortFilters.Games,
                                          getPostsBySortFilters.RateOf,
                                          getPostsBySortFilters.RateTo,
                                          getPostsBySortFilters.Closed,
                                          getPostsBySortFilters.Topics,
                                          getPostsBySortFilters.LanguagesExcluding,
                                          getPostsBySortFilters.GamesExcluding,
                                          getPostsBySortFilters.TopicsExcluding,
                                          getPostsBySortFilters.SearchString);

            var answer = await Sender.Send(query);
            _logger.LogInformation($"Someone Get Posts");
            return Ok(answer);
        }

        [AllowAnonymous]
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetForumById(Guid postId)
        {
            var post = await Sender.Send(new GetPostByIdQuery(postId));
            _logger.LogInformation($"Someone Get Post {postId}");
            return Ok(post);
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteForumById(Guid id)
        {
            var authorPost = await Sender.Send(new GetPostAuthorQuery(id));

            if (authorPost != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                return Forbid();
            }

            var query = new DeleteForumCommand(id);
            await Sender.Send(query);
            _logger.LogInformation($"User {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} delete post");
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPost()]
        public async Task<IActionResult> CloseOrOpenForumById(Guid id, bool close)
        {
            var authorPost = await Sender.Send(new GetPostAuthorQuery(id));

            if (authorPost is null)
            {
                return Forbid();
            }

            if (authorPost != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                return Forbid();
            }

            var query = new CloseOrOpenForumCommand(id, close);
            await Sender.Send(query);
            _logger.LogInformation($"User {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} post is close {close}");
            return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPost("rating")]
        public async Task<IActionResult> PostAddRating(Guid postId, string rating)
        {
            await Sender.Send(new UpdateRatingComplitationCommand(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), postId, rating));
            _logger.LogInformation($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} add rating");
            return Ok();
        }

        #region MessagePost

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("messagePost/add")]
        public async Task<IActionResult> CreateMessagePost(string content, Guid postId)
        {
            var query = new CreateMessagePostCommand(content, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), postId);
            var queryResult = await Sender.Send(query);

            if (!string.IsNullOrWhiteSpace(queryResult.ErrorMessage))
            {
                return Forbid(queryResult.ErrorMessage);
            }

            // send owner notification
            var authorPost = await Sender.Send(new GetPostAuthorQuery(postId));

            if (authorPost != HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)
            {
                var queryNotification = new CreateProfileNotificationComand(new Guid(authorPost), $"MessagePost {postId}");
                await Sender.Send(queryNotification);
            }

            var queryUpdateTime = new UpdateTimeUpdateCommand(postId);
            await Sender.Send(queryUpdateTime);
            _logger.LogInformation($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} add message post");
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
            _logger.LogInformation($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} delete message post");
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
            var messagePostResult = await Sender.Send(query);

            if (!string.IsNullOrWhiteSpace(messagePostResult.ErrorMessage))
            {
                return Forbid(messagePostResult.ErrorMessage);
            }
            _logger.LogInformation($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} update message post {messagePostID}");
            return Ok();
        }
        #endregion

        #region Replies

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpPut("replie/add")]
        public async Task<IActionResult> CreateReplie(string content, Guid messagePostId)
        {
            var query = new CreateReplieCommand(content, new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value), messagePostId);
            var replie = await Sender.Send(query);

            if (!string.IsNullOrWhiteSpace(replie.ErrorMessage))
            {
                return Forbid($"{replie.ErrorMessage}");
            }

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
            _logger.LogInformation($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} add replie for message post {messagePostId}");
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
            _logger.LogInformation($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} delete replie");
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

            if (!string.IsNullOrWhiteSpace(replie.ErrorMessage))
            {
                return Forbid(replie.ErrorMessage);
            }
            _logger.LogInformation($"User by Id {HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value} update replie {replieId}");
            return Ok();
        }
        #endregion
    }
}
