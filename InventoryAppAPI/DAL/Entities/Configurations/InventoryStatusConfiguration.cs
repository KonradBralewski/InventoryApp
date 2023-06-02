using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations
{
    public class StockItemsConfiguration : IEntityTypeConfiguration<InventoryStatus>
    {
        public void Configure(EntityTypeBuilder<InventoryStatus> eb)
        {
            eb.Property(pc => pc.IsActive).HasDefaultValue(false).IsRequired();
        }
    }
}
