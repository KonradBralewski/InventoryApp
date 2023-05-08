using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Requests.Add
{
    public class UpdateLocationRequest
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int RoomId { get; set; }
    }
}
