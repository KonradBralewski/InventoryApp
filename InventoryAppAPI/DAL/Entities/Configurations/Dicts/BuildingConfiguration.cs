using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.DAL.Entities.Configurations.Dicts
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
