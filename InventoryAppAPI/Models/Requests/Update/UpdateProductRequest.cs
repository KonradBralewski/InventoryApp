using InventoryAppAPI.DAL.Entities;

namespace InventoryAppAPI.Models.Requests.Add
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public IEnumerable<StockItems> StockItems { get; set; }
    }
}
