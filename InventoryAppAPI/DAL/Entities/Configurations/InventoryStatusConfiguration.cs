using InventoryAppAPI.DAL.Entities.Dicts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations
{
    public class InventoryStatusConfiguration : IEntityTypeConfiguration<InventoryStatus>
    {
        public void Configure(EntityTypeBuilder<InventoryStatus> eb)
        {
            eb.Property(pc => pc.IsActive).HasDefaultValue(false).IsRequired();
            eb.HasOne(pc => pc.InventoryStatusDict).WithMany(pc => pc.InventoryStatus).HasForeignKey(pc => pc.StatusId);
            eb.HasOne(pc => pc.Inventory).WithOne(pc => pc.Status);
        }
    }
}
