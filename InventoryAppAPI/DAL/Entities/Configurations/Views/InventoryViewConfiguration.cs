using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.Views
{
    public class InventoryViewConfiguration : IEntityTypeConfiguration<InventoryDTO>
    {
        public void Configure(EntityTypeBuilder<InventoryDTO> eb)
        {
            eb.HasKey(x => x.InventoryId);
            eb.ToView("vInventoryList");
        }
    }
}
