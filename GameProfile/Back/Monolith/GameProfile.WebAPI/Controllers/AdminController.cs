using GameProfile.Application.CQRS.Forum.Commands.Delete;
using GameProfile.Application.CQRS.Forum.PostMessage.Commands.Delete;
using GameProfile.Application.CQRS.Forum.Replie.Commands.Delete;
using GameProfile.Application.CQRS.Games.Commands.DeleteGame;
using GameProfile.Application.CQRS.Games.GameComments.Commands.Delete;
using GameProfile.Application.CQRS.Games.GameComments.CommentReplies.Commands.Delete;
using GameProfile.Application.CQRS.Profiles.Role.Command.CreateRole;
using GameProfile.Application.CQRS.Profiles.Role.Request.DoesHaveHaveRoleAdmin;
using GameProfile.WebAPI.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameProfile.WebAPI.Controllers
{
    [Route("admin")]
    public sealed class AdminController : ApiController
    {
        public AdminController(ISender sender)
            : base(sender)
        {

        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpGet]
        public async Task<IActionResult> HaveAdmin()
        {         
            var isAdmin = await Sender.Send(new DoesHaveHaveRoleAdminQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)));

           if (!isAdmin)
            {
                return Forbid();
            }

           return Ok();
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizeRedisCookieAttribute))]
        [HttpDelete]
        public async Task<IActionResult> DeleteByAdmin(Guid id, string name)
        {
            var isAdmin = await Sender.Send(new DoesHaveHaveRoleAdminQuery(new Guid(HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value)));

            if (!isAdmin)
            {
                return Forbid();
            }

            if(name == "Game")
            {
                await Sender.Send(new DeleteGameCommand(id));
            }

            if(name == "Post")
            {
                await Sender.Send(new DeleteForumCommand(id));
            }

            if (name == "PostMessage")
            {
                await  Sender.Send(new DeleteMessagePostCommand(id));
            }

            if (name == "PostReplie")
            {
                await Sender.Send(new DeleteReplieCommand(id));
            }

            if (name == "GameComment")
            {
                await Sender.Send(new DeleteGameCommentCommand(id));
            }

            if (name == "GameReplie")
            {
                await Sender.Send(new DeleteGameReplieCommand(id));
            }

            return Ok();
        }

    }
}
