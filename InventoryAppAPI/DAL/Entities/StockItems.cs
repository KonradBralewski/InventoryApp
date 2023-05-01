using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.DAL.Entities
{
    public class StockItems
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
        public bool IsArchive { get; set; }
        public string InventoriedBy { get; set; }
        public DateTime? InventoriedAt { get; set; }
    }
}
