using InventoryAppAPI.DAL.Entities.Base;

namespace InventoryAppAPI.DAL.Entities.Dicts
{
    public class Location : BaseEntity
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
