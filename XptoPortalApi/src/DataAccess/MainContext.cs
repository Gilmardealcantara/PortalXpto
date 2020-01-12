using Microsoft.EntityFrameworkCore;
using XptoPortalApi.Models;
using XptoPortalApi.DataAcess.Mappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace XptoPortalApi.DataAcess
{
    public class MainContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<App> Apps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppMap());
            builder.ApplyConfiguration(new ApplicationUserMap());
            base.OnModelCreating(builder);
        }

    }
}
