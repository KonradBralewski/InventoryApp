using InventoryAppAPI.DAL.Entities.Base;

namespace InventoryAppAPI.DAL.Entities.Dicts
{
    public class Room : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
