using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities
{
    [Table("ScannedItems")]
    public class ScannedItems
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public Inventories Inventories { get; set; }
        public string Code { get; set; }
        public int StockItemId { get; set; }
        public StockItems StockItems { get; set; }
        public bool isArchive { get; set; }
        public string InventoriedBy { get; set; }
        public DateTime? InventoriedAt { get; set; }
    }
}
