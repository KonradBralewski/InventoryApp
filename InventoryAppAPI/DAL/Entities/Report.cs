using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Entities
{
    [Table("Reports")]
    public class Report
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }

        public byte[] FileData { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int CreatorId { get; set; }
    }
}
