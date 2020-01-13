using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using XptoPortalApi.Models;
using Xunit;

namespace XptoPortalApi.Tests.Integration.Controllers
{
    public class AppIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public IList<App> appFakeMocks { get; private set; }

        public AppIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", factory.Token);
            appFakeMocks = factory.AppFakeMocks;
        }

        [Fact(DisplayName = "App Should Be Caugh")]
        public async Task App_Should_Be_Caugh()
        {
            var httpResponse = await _client.GetAsync("Apps");
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            var dataList = await httpResponse.Content.ReadAsAsync<IList<App>>();
            Assert.Contains(dataList, x => appFakeMocks.Any(y => y == x));
        }
    }
}
