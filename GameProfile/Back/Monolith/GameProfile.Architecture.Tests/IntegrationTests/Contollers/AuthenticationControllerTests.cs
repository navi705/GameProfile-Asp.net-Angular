using GameProfile.Application.Data;

namespace GameProfile.Unit.Tests.IntegrationTests.Contollers
{
    public sealed class AuthenticationControllerTests : IntegrationControllerTest
    {
        public AuthenticationControllerTests(IDatabaseContext context) : base(context)
        {

        }

        [Fact]
        private async void LoginBySteam()
        {
            var result = HttpClient.PostAsync("/login/steam", null).Result;



        }

    }
}
