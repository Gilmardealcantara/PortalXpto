using System;
using Microsoft.EntityFrameworkCore;
using XptoPortalApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace XptoPortalApi.DataAcess.Mappings
{
    public class AppMap : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder.ToTable("Apps");

            builder.Property(p => p.Url).HasColumnType("varchar(500)").IsRequired();
            builder.Property(p => p.Title).HasColumnType("varchar(50)").IsRequired();

            builder.HasData(
                new App { Id = 1, Title = "DTI", Url = "https://dtidigital.com.br/" },
                new App { Id = 2, Title = "wikipedia", Url = "https://www.wikipedia.org/" }
            );
        }
    }
}
