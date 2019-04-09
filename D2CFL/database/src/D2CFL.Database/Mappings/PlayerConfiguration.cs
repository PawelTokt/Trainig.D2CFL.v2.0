using System;
using D2CFL.Database.Extensions;
using D2CFL.Database.Interfaces;
using D2CFL.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace D2CFL.Database.Mappings
{
    public class PlayerConfiguration : IEntityTypeConfiguration<PlayerEntity, Guid>
    {
        public void Map(EntityTypeBuilder<PlayerEntity> entityTypeBuilder)
        {
            // Table
            entityTypeBuilder.ToTable("Article", MsSqlExtensions.Schema.SchemaName);

            // Primary Key
            entityTypeBuilder.HasKey(x => x.Id);

            // Properties
            entityTypeBuilder.Property(x => x.Id).HasDefaultValueSql(MsSqlExtensions.Functions.NewSequentialId);
            entityTypeBuilder.Property(x => x.Name).HasColumnType(MsSqlExtensions.ColumnTypes.GetNVarCharWithSpecifiedLength(MsSqlExtensions.ColumnLengths.UniqueName)).IsRequired();

            // Relationships
            //entityTypeBuilder.HasOne(x => x.Mandator)
            //    .WithMany()
            //    .HasForeignKey(x => x.MandatorId);
        }
    }
}
