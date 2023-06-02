using InventoryAppAPI.DAL.Entities.Dicts;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities
{
    [Table("StockItems")]
    public class StockItem
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } // navigation property
        public int LocationId { get; set; }
        public Location Location { get; set; } // navigation property
        public bool IsArchive { get; set; }

        public ScannedItem ScannedItem { get; set; } // navigation property
        public string InventoriedBy { get; set; }
        public DateTime? InventoriedAt { get; set; }
    }
}
