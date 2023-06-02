using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Requests.Add
{
    public class AddLocationRequest
    {
        public int BuildingId { get; set; }
        public int RoomId { get; set; }
        public string RoomDescription { get; set; }
    }
}
