using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.Views
{
    public class InventoriedStockItemsViewConfiguration : IEntityTypeConfiguration<InventoriedStockItemDTO>
    {
        public void Configure(EntityTypeBuilder<InventoriedStockItemDTO> eb)
        {
            eb.HasKey(x => x.Id);
            eb.ToView("vInventoriedStockItemsList");
        }
    }
}
