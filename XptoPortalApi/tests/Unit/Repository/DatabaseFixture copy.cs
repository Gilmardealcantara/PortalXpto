using System;
using Bogus;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using XptoPortalApi.DataAcess;
using XptoPortalApi.Models;

namespace XptoPortalApi.Tests.Unit.Repository
{
    public class DatabaseFixture2 : IDisposable
    {
        public MainContext Context { get; private set; }
        public IList<App> AppFakeMocks { get; private set; }

        public void PopulateTestData2(MainContext dbContext)
        {
            AppFakeMocks = new Faker<App>()
                .RuleFor(p => p.Title, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .RuleFor(p => p.Url, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .Generate(10);

            dbContext.Apps.AddRange(AppFakeMocks);
            dbContext.SaveChanges();
        }

        public DatabaseFixture2()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
            var dbName = $"InMemoryAppDb-Unit-{DateTime.UtcNow}";
            optionsBuilder.UseInMemoryDatabase(dbName);
            Context = new MainContext(optionsBuilder.Options);
            Context.Database.EnsureCreated();
            this.PopulateTestData2(Context);
        }
        public void Dispose()
        {
            Context.Database.EnsureCreated();
            Context.Dispose();
        }
    }
}
