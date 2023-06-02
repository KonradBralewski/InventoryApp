using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Responses
{
    public class InventoryStatusDictDTO
    {
        public int Id { get; set; }
        public string StatusName { get; set; }

        public InventoryStatusDictDTO(InventoryStatusDict inventoryStatusDict)
        {
            Id = inventoryStatusDict.Id;
            StatusName = inventoryStatusDict.StatusName;
        }
    }
}
