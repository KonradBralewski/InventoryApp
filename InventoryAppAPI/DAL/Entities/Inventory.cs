using InventoryAppAPI.DAL.Entities.Base;
using InventoryAppAPI.DAL.Entities.Dicts;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities
{
    [Table("Inventories")]
    public class Inventory : BaseEntity
    {
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int UserId { get; set; }
        public string Description { get; set; }
        public InventoryStatus Status { get; set; } // navigation property
    }
}
