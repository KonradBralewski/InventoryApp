using InventoryAppAPI.DAL.Entities.Dicts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.DAL.Entities.Configurations.Dicts
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> eb)
        {
            eb.Property(p => p.Name).HasMaxLength(150).IsRequired();
            eb.HasMany(p => p.StockItems).WithOne(pc => pc.Product).HasForeignKey(pc => pc.ProductId);
            eb.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
            eb.Property(e => e.CreatedBy).HasMaxLength(100);
            eb.Property(e => e.ModifiedBy).HasMaxLength(100);
        }
    }
}
