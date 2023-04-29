﻿using InventoryAppAPI.Entities.Dicts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.Entities.Configurations.Dicts
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> eb)
        {
            eb.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
            eb.Property(e => e.CreatedBy).HasMaxLength(100);
            eb.Property(e => e.ModifiedBy).HasMaxLength(100);
            eb.ToTable<Location>("Locations", "dict");
        }
    }
}
