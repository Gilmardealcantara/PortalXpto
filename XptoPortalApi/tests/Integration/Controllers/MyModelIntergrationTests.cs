using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XptoPortalApi.Models;
using XptoPortalApi.Services.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace XptoPortalApi.Tests.Integration.Controllers
{
    public class AppIntegrationTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public IList<App> _myModelFakeMocks { get; private set; }

        public AppIntegrationTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", factory.Token);
            _myModelFakeMocks = factory.AppFakeMocks;
        }

        [Fact(DisplayName = "App Should Be Caugh")]
        public async Task App_Should_Be_Caugh()
        {
            var httpResponse = await _client.GetAsync("Apps");
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            var dataList = await httpResponse.Content.ReadAsAsync<IList<App>>();
            Assert.Contains(dataList, x => _myModelFakeMocks.Any(y => y == x));
        }

        // [Fact(DisplayName = "App Should Be Posted")]
        // public async Task App_Should_Be_Posted()
        // {
        //     var myModel = JObject.Parse(@"{
        //         'prop1': 1,
        //         'prop2': 'Key Test',
        //         'prop3': 1.2,
        //         'prop4': 2,
        //     }");
        //     var userContent = new StringContent(JsonConvert.SerializeObject(myModel), Encoding.UTF8, "application/json");
        //     var httpResponse = await _client.PostAsync("App", userContent);
        //     Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
        //     var success = await httpResponse.Content.ReadAsAsync<bool>();
        //     Assert.True(success);
        // }

        // [Fact(DisplayName = "App Should Be CaughtByID")]
        // public async Task App_Should_Be_CaughtByID()
        // {
        //     var originalData = _myModelFakeMocks.First();
        //     var httpResponse = await _client.GetAsync($"App/{originalData.Id}");
        //     Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        //     var data = await httpResponse.Content.ReadAsAsync<App>();

        //     Assert.Equal(originalData, data);
        //     Assert.Equal(JsonService.Serialize(originalData), JsonService.Serialize(data));

        // }

        // [Fact(DisplayName = "App Should Be Put")]
        // public async Task App_Should_Be_Put()
        // {
        //     var newModel = JObject.Parse(@"{
        //         'prop1': 2,
        //         'prop2': 'New Test',
        //         'prop3': 3.0
        //     }");

        //     var userContent = new StringContent(JsonConvert.SerializeObject(newModel), Encoding.UTF8, "application/json");
        //     var httpResponse = await _client.PutAsync("App/2", userContent);

        //     Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        //     var success = await httpResponse.Content.ReadAsAsync<bool>();
        //     Assert.True(success);
        // }


        // [Fact(DisplayName = "App Should Be Deleted")]
        // public async Task App_Should_Be_Deleted()
        // {
        //     var httpResponse = await _client.DeleteAsync("App/3");
        //     Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
        //     var success = await httpResponse.Content.ReadAsAsync<bool>();
        //     Assert.True(success);
        // }
    }
}
