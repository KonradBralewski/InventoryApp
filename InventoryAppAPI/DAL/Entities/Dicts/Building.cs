using InventoryAppAPI.DAL.Entities.Base;

namespace InventoryAppAPI.DAL.Entities.Dicts
{
    public class Building : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
