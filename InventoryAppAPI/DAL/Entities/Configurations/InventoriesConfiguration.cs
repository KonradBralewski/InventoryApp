using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations
{
    public class InventoriesConfiguration : IEntityTypeConfiguration<Inventories>
    {
        public void Configure(EntityTypeBuilder<Inventories> eb)
        {
            eb.Property(pc => pc.Description).HasMaxLength(255);
        }
    }
}
