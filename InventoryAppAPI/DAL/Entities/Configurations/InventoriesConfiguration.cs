using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations
{
    public class InventoriesConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> eb)
        {
            eb.Property(pc => pc.Description).HasMaxLength(255);
        }
    }
}
