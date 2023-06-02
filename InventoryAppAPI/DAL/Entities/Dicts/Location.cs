using InventoryAppAPI.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities.Dicts
{
    [Table("Locations", Schema = "dict")]
    public class Location : BaseEntity
    {
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        public int RoomNo { get; set; }
        public string RoomDescription { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
