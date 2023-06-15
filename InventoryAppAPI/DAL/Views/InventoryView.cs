using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Entities;

namespace InventoryAppAPI.DAL.Views
{
    public class InventoryView
    {
        public int InventoryId { get; set; }
        public int BuildingId { get; set; }
        public int LocationId { get; set; }

        public string BuildingName { get; set; }
        public string RoomDescription { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public string StatusName { get; set; }

        public bool IsActive { get; set; }
    }
}
