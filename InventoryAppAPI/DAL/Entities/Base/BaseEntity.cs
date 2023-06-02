namespace InventoryAppAPI.DAL.Entities.Base
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
