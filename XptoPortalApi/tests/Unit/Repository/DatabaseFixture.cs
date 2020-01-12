using System;
using Bogus;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using XptoPortalApi.DataAcess;
using XptoPortalApi.Models;

namespace XptoPortalApi.Tests.Unit.Repository
{
    public class DatabaseFixture : IDisposable
    {
        public MainContext Context { get; private set; }
        public IList<App> AppFakeMocks { get; private set; }

        public void PopulateTestData(MainContext dbContext)
        {
            AppFakeMocks = new Faker<App>()
                .RuleFor(p => p.Title, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .RuleFor(p => p.Url, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .Generate(10);

            dbContext.Apps.AddRange(AppFakeMocks);
            dbContext.SaveChanges();
        }

        public DatabaseFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
            var dbName = $"InMemoryAppDb-{DateTime.UtcNow}";
            Console.WriteLine($"Generate data for {dbName}");
            optionsBuilder.UseInMemoryDatabase(dbName);
            Context = new MainContext(optionsBuilder.Options);
            Context.Database.EnsureCreated();
            this.PopulateTestData(Context);
        }
        public void Dispose()
        {
            Context.Database.EnsureCreated();
            Context.Dispose();
        }
    }
}
