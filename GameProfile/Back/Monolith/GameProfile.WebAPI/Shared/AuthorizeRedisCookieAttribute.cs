using GameProfile.Application;
using GameProfile.Persistence.Caching;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GameProfile.WebAPI.Shared
{
    public sealed class AuthorizeRedisCookieAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly ICacheService _cacheService;

        public AuthorizeRedisCookieAttribute(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userCache = await _cacheService.GetAsync<UserCache>(context.HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
            if (!userCache.DeviceList.Any(device => device.UserAgent == context.HttpContext.Request.Headers.UserAgent && device.SessionCookie == context.HttpContext.Request.Cookies[".Auth.Cookies"]))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
