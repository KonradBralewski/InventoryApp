using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Responses
{
    public class LocationDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int BuildingId { get; set; }
        public string RoomDescription { get; set; }

        public LocationDTO(Location location)
        {
            Id = location.Id;
            RoomId = location.RoomNo;
            BuildingId = location.BuildingId;
            RoomDescription = location.RoomDescription;
        }
    }
}
