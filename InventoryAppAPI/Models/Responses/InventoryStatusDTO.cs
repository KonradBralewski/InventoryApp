using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Responses
{
    public class InventoryStatusDTO
    {
        public int Id { get; set; }
        public InventoryStatusDictDTO Status { get; set; }
        public int InventoryId { get; set; }

        public bool IsActive { get; set; }

        public InventoryStatusDTO(InventoryStatus inventoryStatus, InventoryStatusDict inventoryStatusDict)
        {
            Id = inventoryStatus.Id;
            Status = new InventoryStatusDictDTO(inventoryStatusDict);
            InventoryId = inventoryStatus.InventoryId;
            IsActive = inventoryStatus.IsActive;
        }
    }
}
