using InventoryAppAPI.DAL.Entities.Dicts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.Dicts
{
    public class InventoryStatusDictConfiguration : IEntityTypeConfiguration<InventoryStatusDict>
    {
        public void Configure(EntityTypeBuilder<InventoryStatusDict> eb)
        {
            eb.Property(p => p.StatusName).HasMaxLength(255).IsRequired();
        }
    }
}
