using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities.Dicts
{
    [Table("InventoryStatus", Schema = "dict")]
    public class InventoryStatusDict
    {
        public int Id { get; set; }
        public string StatusName { get; set; }
        public ICollection<InventoryStatus> InventoryStatus { get; set; }
    }
}
