using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities
{
    [Table("ScannedItems")]
    public class ScannedItem
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public Inventory Inventory { get; set; } // navigation property
        public string Code { get; set; }
        public int StockItemId { get; set; }
        public StockItem StockItem { get; set; } // navigation property
        public bool isArchive { get; set; }
        public string InventoriedBy { get; set; }
        public DateTime? InventoriedAt { get; set; }
    }
}
