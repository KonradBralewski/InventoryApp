using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Responses
{
    public class StockItemDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public bool IsArchive { get; set; }
        public string InventoriedBy { get; set; }
        public DateTime? InventoriedAt { get; set; }
    }
}
