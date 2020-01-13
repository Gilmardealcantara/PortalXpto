using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using System.Reflection;
using Swashbuckle.AspNetCore.Swagger;

using XptoPortalApi.DataAcess;
using XptoPortalApi.DataAcess.Interfaces;
using XptoPortalApi.Api.WS;
using XptoPortalApi.Api.Middlewares;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using XptoPortalApi.DataAcess.Repository;
using System.Collections.Generic;
using System.Linq;
using XptoPortalApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace XptoPortalApi
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _config;
        private readonly ILogger _logger;

        public Startup(IHostingEnvironment env, IConfiguration config, ILoggerFactory loggerFactory)
        {
            _env = env;
            _config = config;
            _logger = loggerFactory.CreateLogger<Startup>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            string secret = _config["Application:SECRET_KEY"];
            string issuer = _config["Application:JWT_ISSUE"];
            string audience = _config["Application:JWT_AUDIENCE"];
            string urlConnection = _config["CONNECTION_STRING"];

            services.AddDbContext<MainContext>(options => options.UseMySql(_config["CONNECTION_STRING"], x =>
            {
                x.MigrationsHistoryTable("__XptoPortalApiMigrationsHistory");
                x.MigrationsAssembly("XptoPortalApi.EFMigrate");
            }));

            services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddRoleStore<RoleStore<IdentityRole<int>, MainContext, int>>()
            .AddUserStore<UserStore<ApplicationUser, IdentityRole<int>, MainContext, int>>()
            .AddEntityFrameworkStores<MainContext>()
            .AddDefaultTokenProviders();

            // services.AddDbContext<MainContext>(options => options.UseInMemoryDatabase("app.db"));
            services.AddMemoryCache();

            /* Repositories */
            services.AddTransient<IAppsRepo, AppsRepo>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            }).AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        // Console.WriteLine("Token Invalido ..:. " + context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        // Console.WriteLine("Token válido ..:. " + context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression(options =>
             {
                 options.EnableForHttps = true;
                 options.Providers.Add<GzipCompressionProvider>();
             });
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            });

            services.AddTransient<WebSocketConnectionManager>();
            foreach (var type in Assembly.GetEntryAssembly().ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == typeof(WebSocketHandler))
                {
                    services.AddSingleton(type);
                }
            }

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "XptoPortalApi API", Version = "v1" });
                x.AddSecurityDefinition("Bearer",
                new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                x.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    { "Bearer", Enumerable.Empty<string>() },
                });
            });

            services.AddHealthChecks();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddXmlSerializerFormatters();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionMiddleware>(_logger);
            app.UseHttpsRedirection();
            app.UseHealthChecks("/Health");

            app.UseSwagger(option => option.RouteTemplate = _config["SwaggerOptions:JsonRoute"]);
            app.UseSwaggerUI(option => option.SwaggerEndpoint(_config["SwaggerOptions:UiEndPoint"], _config["SwaggerOptions:Description"]));

            app.UseCors("CorsPolicy");
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
