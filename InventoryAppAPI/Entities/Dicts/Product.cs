namespace InventoryAppAPI.Entities.Dicts
{
    public class Product 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StockItems> StockItems { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
