
using Xunit;
using Moq;
using Bogus;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using XptoPortalApi.Models;
using XptoPortalApi.Services;
using XptoPortalApi.DataAcess.Interfaces;
using XptoPortalApi.Api.Controllers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using XptoPortalApi.Services.Utils;

namespace XptoPortalApi.Tests.Unit.Services
{
    public class AppControllerUnitTests
    {
        private readonly AppsController _myModelController;
        public AppControllerUnitTests()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var mockRepo = this.MockAppsRepo();
            _myModelController = new AppsController(this.MockAppsRepo());
        }

        private IAppsRepo MockAppsRepo()
        {
            var mockDependency = new Mock<IAppsRepo>();

            var data = new Faker<App>()
                .RuleFor(p => p.Title, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .RuleFor(p => p.Url, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .Generate(10).ToAsyncEnumerable();

            mockDependency.Setup(x => x.Select()).Returns(data);
            mockDependency.Setup(x => x.Select(It.Is<int>(y => y == 1))).Returns(data.First());
            mockDependency.Setup(x => x.Insert(It.IsAny<App>())).Returns(Task.FromResult(true));
            mockDependency.Setup(x => x.Update(It.IsAny<App>())).Returns(Task.FromResult(true));
            mockDependency.Setup(x => x.Delete(It.IsInRange<int>(1, 10, Range.Inclusive))).Returns(Task.FromResult(true));

            return mockDependency.Object;
        }

        [Fact(DisplayName = "AppController Should Be Caught")]
        public async Task AppController_Should_Be_Get()
        {
            var response = (await _myModelController.Get() as ObjectResult);
            Assert.NotNull(response);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
            var data = (response.Value as App[]);
            Assert.NotNull(data);
            Assert.Equal(10, data.Count());
        }
    }
}
