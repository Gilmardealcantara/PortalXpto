using System;
using XptoPortalApi.Models;
using XptoPortalApi.DataAcess;
using Bogus;
using System.Collections.Generic;

namespace XptoPortalApi.Tests.Integration
{
    public static class SeedData
    {
        public static IList<App> PopulateTestData(MainContext dbContext)
        {
            var myModelFakeMocks = new Faker<App>()
                .RuleFor(p => p.Id, p => p.IndexFaker + 3)
                .RuleFor(p => p.Url, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .RuleFor(p => p.Title, p => p.Random.String2(1, 10, "qwertyuiopasdfghjklzxcvbnm1234567890"))
                .Generate(10);

            dbContext.Apps.AddRange(myModelFakeMocks);
            dbContext.SaveChanges();
            return myModelFakeMocks;
        }
    }
}
