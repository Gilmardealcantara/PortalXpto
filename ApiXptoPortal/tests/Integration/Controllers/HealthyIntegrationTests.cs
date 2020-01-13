using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace XptoPortalApi.Tests.Integration.Controllers
{
    public class HealthyIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public HealthyIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", factory.Token);
        }

        [Fact]
        public async Task CanGetHealthy()
        {
            // The endpoint or route of the controller action.
            var httpResponse = await _client.GetAsync("/Health");

            // Must be successful.
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal("Healthy", stringResponse);
        }
    }
}
