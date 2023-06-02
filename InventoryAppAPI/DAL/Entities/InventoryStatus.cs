using InventoryAppAPI.DAL.Entities.Dicts;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities
{
    [Table("InventoryStatus")]
    public class InventoryStatus
    {
        public int StatusId { get; set; }
        public InventoryStatusDict InventoryStatusDict {get; set;}
        public int InventoryId { get; set; }

        public Inventories Inventories { get; set; }
        public bool IsActive { get; set; }
    }
}
