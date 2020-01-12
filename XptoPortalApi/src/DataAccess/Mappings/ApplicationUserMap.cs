using System;
using Microsoft.EntityFrameworkCore;
using XptoPortalApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace XptoPortalApi.DataAcess.Mappings
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Ignore(x => x.Token);
            builder.HasData(
                new ApplicationUser
                {
                    Id = 1,
                    UserName = "gilmardealcantara",
                    Name = "Gilmar de Alcantara",
                    Email = "gilmardealcantara@gmail.com",
                }
            );
        }
    }
}
