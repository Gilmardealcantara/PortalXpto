using Microsoft.EntityFrameworkCore;
using XptoPortalApi.Models;
using XptoPortalApi.DataAcess.Mappings;

namespace XptoPortalApi.DataAcess
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options) { }
        public DbSet<App> Apps { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AppMap());
        }

    }
}
