using InventoryAppAPI.Entities.Dicts;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace InventoryAppAPI.Entities.Configurations.Dicts
{
    public class BuildingConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> eb)
        {
            eb.Property(r => r.Name).HasMaxLength(150).IsRequired();
            eb.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
            eb.Property(e => e.CreatedBy).HasMaxLength(100);
            eb.Property(e => e.ModifiedBy).HasMaxLength(100);
            eb.ToTable("Buildings", "dict");
        }
    }
}
