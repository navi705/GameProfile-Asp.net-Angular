using Microsoft.AspNetCore.Mvc.Testing;

namespace GameProfile.Unit.Tests.IntegrationTests.Contollers
{
    public class IntegrationControllerTest
    {
        protected readonly HttpClient HttpClient;

        public IntegrationControllerTest()
        {
            var appFactory = new WebApplicationFactory<Program>();
            HttpClient = appFactory.CreateClient();
        }
    }
}
