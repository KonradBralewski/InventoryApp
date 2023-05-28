using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Requests.Add
{
    public class AddStockItemRequest
    {
        public string Code { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public bool IsArchive { get; set; }
    }
}
