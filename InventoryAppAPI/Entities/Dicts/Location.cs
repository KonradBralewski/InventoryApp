namespace InventoryAppAPI.Entities.Dicts
{
    public class Location 
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public Building Building { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
