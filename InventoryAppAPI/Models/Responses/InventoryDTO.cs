using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Entities;

namespace InventoryAppAPI.Models.Responses
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; }
        public int LocationId { get; set; }

        public int UserId { get; set; }
        public string Description { get; set; }
        public int StatusName { get; set; }
        
        public bool IsActive { get; set; }
    }
}
