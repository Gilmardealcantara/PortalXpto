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
            builder.ToTable("XptoPortalApiApps");

            builder.Property(p => p.Url).HasColumnType("varchar(500)").IsRequired();
            builder.Property(p => p.Title).HasColumnType("varchar(50)").IsRequired();
        }
    }
}
