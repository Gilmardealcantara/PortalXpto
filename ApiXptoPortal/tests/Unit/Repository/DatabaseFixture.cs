using System;
using Bogus;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using XptoPortalApi.DataAcess;
using XptoPortalApi.Models;

namespace XptoPortalApi.Tests.Unit.Repository
{
    public static class DatabaseFixture
    {
        public static MainContext Context { get; private set; }
        public static IList<App> AppFakeMocks { get; private set; }

        public static void PopulateTestData(MainContext dbContext)
        {
            AppFakeMocks = new Faker<App>()
                .RuleFor(p => p.Id, p => p.IndexFaker + 3)
                .RuleFor(p => p.Title, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .RuleFor(p => p.Url, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .Generate(10);

            dbContext.Apps.AddRange(AppFakeMocks);
            dbContext.SaveChanges();
        }

        public static void GenerateDataBase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MainContext>();
            var dbName = $"InMemoryAppDb-Unit-{DateTime.UtcNow.Millisecond}";
            Console.WriteLine($"Generate data for {dbName}");
            optionsBuilder.UseInMemoryDatabase(dbName);
            Context = new MainContext(optionsBuilder.Options);
            Context.Database.EnsureCreated();
            DatabaseFixture.PopulateTestData(Context);
        }
    }
}
