using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GameProfile.Infrastructure.Steam;
using System.Security.Claims;


namespace GameProfile.WebAPI.Controllers
{
    public sealed class AuthenticationController : ApiController
    {
        public AuthenticationController(ISender sender) : base(sender)
        {
        }

        [AllowAnonymous]
        [HttpPost("login/steam")]
        public async Task<IActionResult> LoginBySteam(SteamOpenIdData steamOpenIdData)
        {
            SteamApi steamApi = new();
            string idUser = steamOpenIdData.openidclaimed_id.Substring(37);
            bool authIsValid = await steamApi.CheckOpenIdSteam(steamOpenIdData);
            if(authIsValid == false)
            {
                return Unauthorized();
            }
            string avatar = await steamApi.SteamUserGetPlayerSummaries(idUser);
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, idUser)};
            // создаем объект ClaimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            // установка аутентификационных куки
            var requestContent = Request.HttpContext;
            await requestContent.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            var anser = new AnswerLoginSteam() {Id = idUser, avatar = avatar}; 
            return Ok(anser);

        }

        
        [HttpPost("logout")]
        //[Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

    }
    class AnswerLoginSteam
    {
        public string Id { get; set; }
        public string avatar { get; set; }
    }
}
