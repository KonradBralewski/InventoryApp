using InventoryAppAPI.DAL.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.Views
{
    public class InventoryViewConfiguration : IEntityTypeConfiguration<InventoryView>
    {
        public void Configure(EntityTypeBuilder<InventoryView> eb)
        {
            eb.HasKey(x => x.InventoryId);
            eb.ToView("vInventoryList");
        }
    }
}
