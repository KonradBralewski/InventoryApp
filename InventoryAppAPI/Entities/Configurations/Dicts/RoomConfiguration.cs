using InventoryAppAPI.Entities.Dicts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryAppAPI.Entities.Configurations.Dicts
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> eb)
        {
            eb.Property(r => r.Name).HasMaxLength(150).IsRequired();
            eb.Property(e => e.CreatedAt).HasDefaultValueSql("getdate()");
            eb.Property(e => e.CreatedBy).HasMaxLength(100);
            eb.Property(e => e.ModifiedBy).HasMaxLength(100);
            eb.ToTable("Rooms", "dict");
        }
    }
}
