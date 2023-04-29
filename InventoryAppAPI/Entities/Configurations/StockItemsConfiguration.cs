using InventoryAppAPI.Entities.Dicts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.Entities.Configurations
{
    public class StockItemsConfiguration : IEntityTypeConfiguration<StockItems>
    {
        public void Configure(EntityTypeBuilder<StockItems> eb)
        {
            eb.Property(pc => pc.Code).HasMaxLength(100).IsRequired();
            eb.Property(pc => pc.IsArchive).HasDefaultValue(false);
            eb.ToTable("StockItems");
        }
    }
}
