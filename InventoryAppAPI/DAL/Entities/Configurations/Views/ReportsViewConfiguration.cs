using InventoryAppAPI.DAL.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.Views
{
    public class ReportViewConfiguration : IEntityTypeConfiguration<ReportView>
    {
        public void Configure(EntityTypeBuilder<ReportView> eb)
        {
            eb.HasKey(x => x.Id);
            eb.ToView("vReportsList");
        }
    }
}
