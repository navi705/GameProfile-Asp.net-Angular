using GameProfile.Application.Data;
using Microsoft.AspNetCore.Mvc.Testing;

namespace GameProfile.Unit.Tests.IntegrationTests.Contollers
{
    public class IntegrationControllerTest
    {
        protected readonly HttpClient HttpClient;
        protected readonly IDatabaseContext _context;

        public IntegrationControllerTest(IDatabaseContext context)
        {
            var appFactory = new WebApplicationFactory<Program>();
            HttpClient = appFactory.CreateClient();
            _context = context;
        }
    }
}
