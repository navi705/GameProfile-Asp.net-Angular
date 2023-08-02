using GameProfile.Application;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GameProfile.Unit.Tests.IntegrationTests.Contollers
{
    public sealed class AuthenticationControllerTests : IntegrationControllerTest
    {
        [Fact]
        private async void LoginBySteam()
        {
            var asf = HttpClient.PostAsync("/login/steam", null).Result;
        }

    }
}
