using InventoryAppAPI.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities.Dicts
{
    [Table("Buildings", Schema = "dict")]
    public class Building : BaseEntity
    {
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
