using InventoryAppAPI.DAL.Entities.Base;
using InventoryAppAPI.DAL.Entities.Dicts;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities
{
    [Table("InventoryStatus")]
    public class InventoryStatus : BaseEntity
    {
        public int StatusId { get; set; }
        public InventoryStatusDict InventoryStatusDict {get; set;} // navigation property
        public int InventoryId { get; set; }

        public Inventory Inventory { get; set; } // navigation property
        public bool IsActive { get; set; }
    }
}
