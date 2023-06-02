using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations
{
    public class StockItemsConfiguration : IEntityTypeConfiguration<ScannedItems>
    {
        public void Configure(EntityTypeBuilder<ScannedItems> eb)
        {
            eb.Property(pc => pc.Code).HasMaxLength(255).IsRequired();
            eb.Property(pc => pc.isArchive).HasDefaultValue(false).IsRequired();
        }
    }
}
