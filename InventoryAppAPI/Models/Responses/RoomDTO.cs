using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Responses
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public RoomDTO(Room room)
        {
            Id = room.Id;
            Name = room.Name;
        }
    }
}
