using InventoryAppAPI.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities.Dicts
{
    [Table("Products", Schema = "dict")]
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<StockItems> StockItems { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
