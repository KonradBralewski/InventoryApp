using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.ProcedureModels
{
    public class InventoryViewConfiguration : IEntityTypeConfiguration<ScannedItemDTO>
    {
        public void Configure(EntityTypeBuilder<ScannedItemDTO> eb)
        {
            eb.HasNoKey();
            eb.ToView("FakeView");
        }
    }
}
