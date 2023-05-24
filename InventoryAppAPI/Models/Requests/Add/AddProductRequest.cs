using InventoryAppAPI.DAL.Entities;

namespace InventoryAppAPI.Models.Requests.Add
{
    public class AddProductRequest
    {
        public string Name { get; set; }
        public IEnumerable<StockItems> StockItems { get; set; }
    }
}
