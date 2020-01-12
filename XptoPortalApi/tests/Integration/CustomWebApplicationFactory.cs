using Xunit;
using System;
using System.IO;
using System.Text;
using XptoPortalApi.DataAcess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using XptoPortalApi.Models;


//Optional
[assembly: CollectionBehavior(DisableTestParallelization = true)]
//Optional
[assembly: TestCaseOrderer("Xunit.Extensions.Ordering.TestCaseOrderer", "Xunit.Extensions.Ordering")]
//Optional
[assembly: TestCollectionOrderer("Xunit.Extensions.Ordering.CollectionOrderer", "Xunit.Extensions.Ordering")]


namespace XptoPortalApi.Tests.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    {
        public IConfigurationRoot Config { get; private set; }
        public IList<App> AppFakeMocks { get; private set; }
        public string Token { get; private set; }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<Startup>()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("launchSettings.json")
                .AddEnvironmentVariables()
                .Build();

            var list = Config.GetSection("profiles:XptoPortalApi:environmentVariables").GetChildren()
                    .Select(item => new KeyValuePair<string, string>(item.Key, item.Value))
                    .ToDictionary(x => x.Key, x => x.Value); ;

            foreach (var item in list)
            {
                Environment.SetEnvironmentVariable(item.Key, item.Value);
            }

            builder.UseEnvironment("Staging");

            builder.ConfigureServices(services =>
            {
                //Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                Token = GenerateToken();

                // Add a database context (MainContext) using an in-memory database for testing.
                services.AddDbContext<MainContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryAppDb");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<MainContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();

                    try
                    {
                        // Seed the database with some specific test data.
                        AppFakeMocks = SeedData.PopulateTestData(appDb);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }

        private string GenerateToken()
        {
            string secret = Config["Application:SECRET_KEY"];
            string ecrypt = Config["Application:ENCRYPT_KEY"];
            string issuer = Config["Application:JWT_ISSUE"];
            string audience = Config["Application:JWT_AUDIENCE"];

            var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(secret));
            var encryptKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(ecrypt));

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha512);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Gilar"),
                new Claim(ClaimTypes.NameIdentifier, "1122334455"),
                new Claim(ClaimTypes.Email, "gilmar.alcantara@gmail.com"),
            };

            var handler = new JwtSecurityTokenHandler();

            var jwtSecurityToken = handler.CreateJwtSecurityToken(
                issuer,
                audience,
                new ClaimsIdentity(claims),
                DateTime.Now,
                DateTime.Now.AddDays(2),
                DateTime.Now,
                signingCredentials);

            return handler.WriteToken(jwtSecurityToken);
        }
    }
}
