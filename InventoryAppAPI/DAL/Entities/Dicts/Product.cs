using InventoryAppAPI.DAL.Entities.Base;

namespace InventoryAppAPI.DAL.Entities.Dicts
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StockItems> StockItems { get; set; }
    }
}
