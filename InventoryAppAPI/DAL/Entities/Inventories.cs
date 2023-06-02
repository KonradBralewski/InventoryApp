using InventoryAppAPI.DAL.Entities.Base;
using InventoryAppAPI.DAL.Entities.Dicts;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities
{
    [Table("Inventories")]
    public class Inventories : BaseEntity
    {
        public int LocationId { get; set; }
        public Location Location { get; set; }

        public int UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string Description { get; set; }
    }
}
