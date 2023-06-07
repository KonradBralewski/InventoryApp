using InventoryAppAPI.Models.Requests.Procedures;
using InventoryAppAPI.Models.Responses;

namespace InventoryAppAPI.BLL.Services.Inventory
{
    public interface IInventoryService
    {
        public Task<dynamic> ScanItem(ScanItemRequest request);
        public Task<dynamic> StartInventoryProcess(StartInventoryProcessRequest request);
    }
}
