using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations
{
    public class StockItemsConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> eb)
        {
            eb.Property(pc => pc.Code).HasMaxLength(100).IsRequired();
            eb.Property(pc => pc.IsArchive).HasDefaultValue(false).IsRequired();
            eb.HasOne(pc => pc.ScannedItem).WithOne(pc => pc.StockItem).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
