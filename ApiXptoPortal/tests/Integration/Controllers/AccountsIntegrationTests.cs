using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using XptoPortalApi.Models;
using XptoPortalApi.Services.Utils;
using Xunit;

namespace XptoPortalApi.Tests.Integration.Controllers
{
    public class AccountsIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public IList<App> appFakeMocks { get; private set; }

        public AccountsIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", factory.Token);
            appFakeMocks = factory.AppFakeMocks;
        }

        [Fact(DisplayName = "Accounts Should Be Caugh")]
        public async Task App_Should_Be_Caugh()
        {
            var httpResponse = await _client.GetAsync("Accounts");
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            var dataList = await httpResponse.Content.ReadAsAsync<IList<ApplicationUser>>();
            Assert.True(dataList.Count() > 0);
        }

        [Fact(DisplayName = "User Should Be Logged In")]
        public async Task User_Should_Be_Logged_In()
        {
            var auth = JObject.Parse(@"{
                'email': 'gilmardealcantara@gmail.com',
                'password': '123456'
            }");

            var httpResponse = await _client.PostAsJsonAsync("Accounts/Login", auth);

            Console.WriteLine(httpResponse.Content.ReadAsStringAsync());

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            var user = await httpResponse.Content.ReadAsAsync<ApplicationUser>();
            Assert.NotNull(user);
            Assert.NotNull(user.Email);
            Assert.NotEmpty(user.Email);

            Assert.NotNull(user.UserName);
            Assert.NotEmpty(user.UserName);

            Assert.NotNull(user.Name);
            Assert.NotEmpty(user.Name);
            Console.WriteLine(JsonService.Serialize(user));

            Assert.NotNull(user.Token);
            Assert.NotEmpty(user.Token);
        }
    }
}
