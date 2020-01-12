using System;
using Xunit;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using XptoPortalApi.DataAcess.Interfaces;
using XptoPortalApi.DataAcess.Repository;
using XptoPortalApi.Models;
using XptoPortalApi.Services.Utils;

namespace XptoPortalApi.Tests.Unit.Repository
{
    public class AppRepoUnitTests
    {
        private readonly IAppsRepo _apps;
        private readonly IList<App> _appFakeMocks;
        public AppRepoUnitTests()
        {
            DatabaseFixture.GenerateDataBase();
            _apps = new AppsRepo(DatabaseFixture.Context);
            _appFakeMocks = DatabaseFixture.AppFakeMocks;
        }

        [Fact(DisplayName = "App Should Be Selected")]
        public async Task App_Should_Be_Selected()
        {
            var data = await _apps.Select().ToList();
            Assert.True(data.Count() > 0);
            Console.WriteLine(_appFakeMocks.Count());
            Console.WriteLine(data.Count());
            Assert.True(_appFakeMocks.Count() < data.Count());
        }

        [Fact(DisplayName = "App Should Be Selected ById")]
        public async Task App_Should_Be_Selected_ById()
        {
            var testElement = _appFakeMocks.First();
            var data = await _apps.Select(testElement.Id);
            Assert.Equal(testElement.Id, data.Id);
            Assert.Equal(JsonService.Serialize(testElement), JsonService.Serialize(data));
        }

        [Fact(DisplayName = "App Should Be Inserted")]
        public async Task App_Should_Be_Inserted()
        {
            var app = new App()
            {
                Id = 13,
                Title = "Teste",
                Url = "https://teste/teste",
            };
            var ok = await _apps.Insert(app);
            Assert.True(ok);
        }

        [Fact(DisplayName = "App Should Be Updated")]
        public async Task App_Should_Be_Updated()
        {
            var testData = _appFakeMocks.First();
            testData.Title = "Change Title";
            testData.Url = "https://teste/teste/change";
            var ok = await _apps.Update(testData);
            Assert.True(ok);
        }

        [Fact(DisplayName = "App Should Be Deleted")]
        public async Task App_Should_Be_Deleted()
        {
            var deleteId = _appFakeMocks.Last().Id;
            var ok = await _apps.Delete(deleteId);
            Assert.True(ok);
        }

        [Fact(DisplayName = "App Should Be Get By Query")]
        public async Task App_Should_Be_GetByQuery()
        {
            var filterData = _appFakeMocks.First();
            var numRegisters = await _apps.GetBy(x =>
               x.Id == filterData.Id &&
               x.Title == filterData.Title &&
               x.Url == filterData.Url
            ).Count();

            Assert.True(numRegisters > 0);
        }

        [Theory(DisplayName = "App Should Be Get Paged")]
        [InlineData(1, 10)]
        [InlineData(80, 1)]
        [InlineData(10, 12)]
        [InlineData(2, 40)]
        [InlineData(1, 50)]
        [InlineData(1, 100)]
        [InlineData(2, 100)]
        public async Task App_Should_Be_GetPaged(int page, int pageSize)
        {

            var query = _apps.Select();
            var data = await _apps.GetPaged(query, page, pageSize);

            Assert.True(pageSize >= data.Results.Count());
            Assert.Equal(page, data.CurrentPage);
            Assert.Equal(pageSize, data.PageSize);
            Assert.Equal(12, data.RowCount);
            Assert.Equal(Math.Ceiling(12.00 / pageSize), data.PageCount);
            Assert.Equal((page - 1) * pageSize + 1, data.FirstRowOnPage);
            Assert.Equal(Math.Min(page * pageSize, 12), data.LastRowOnPage);

        }
    }
}
