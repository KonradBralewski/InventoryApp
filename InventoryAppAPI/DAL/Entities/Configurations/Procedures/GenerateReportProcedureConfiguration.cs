using InventoryAppAPI.DAL.Entities.Dicts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Procedures
{
    public class GenerateReportProcedureConfiguration : IEntityTypeConfiguration<GenerateReportProcedure>
    {
        public void Configure(EntityTypeBuilder<GenerateReportProcedure> eb)
        {
            eb.HasNoKey();
            eb.ToView(null);
        }
    }
}
