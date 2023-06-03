using InventoryAppAPI.Models.Requests.Procedures;
using InventoryAppAPI.Models.Responses;

namespace InventoryAppAPI.BLL.Services.Inventory
{
    public interface IInventoryService
    {
        public Task<ScannedItemDTO> ScanItem(ScanItemRequest request);
    }
}
