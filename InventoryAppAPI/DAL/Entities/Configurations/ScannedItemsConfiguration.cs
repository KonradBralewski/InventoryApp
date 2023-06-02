using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations
{
    public class ScannedItemsConfiguration : IEntityTypeConfiguration<ScannedItem>
    {
        public void Configure(EntityTypeBuilder<ScannedItem> eb)
        {
            eb.Property(pc => pc.Code).HasMaxLength(255).IsRequired();
            eb.Property(pc => pc.isArchive).HasDefaultValue(false).IsRequired();
            eb.HasOne(pc => pc.StockItem).WithOne(pc => pc.ScannedItem).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
