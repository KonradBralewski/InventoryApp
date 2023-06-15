using InventoryAppAPI.DAL.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.Views
{
    public class FileViewConfiguration : IEntityTypeConfiguration<FileView>
    {   
        public void Configure(EntityTypeBuilder<FileView> eb)
        {
            eb.HasKey(x => x.Id);
            eb.ToView("vFileList");
        }
    }
}
