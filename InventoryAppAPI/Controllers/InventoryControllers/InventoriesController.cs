using InventoryAppAPI.BLL.Services.Inventory;
using InventoryAppAPI.Controllers.InventoryControllers.Abstract;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Models.Requests.Procedures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    public class InventoriesController : InventoryAppController
    {
        IInventoryRepository _inventoryRepository;
        IInventoryService _inventoryService;

        public InventoriesController(IInventoryRepository inventoryRepository, IInventoryService inventoryService)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryService = inventoryService;
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartInventoryProcessAsync([FromBody] StartInventoryProcessRequest request)
        {
            var result = await _inventoryService.StartInventoryProcess(request);
            return Ok(result);
        }

        [HttpPost("scan")]
        public async Task<IActionResult> ScanItemAsync([FromBody]ScanItemRequest request)
        {
            request.UserId = this.GetCallerId();

            var result = await _inventoryService.ScanItem(request);

            return Ok(result);
        }

        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUserInventories()
        {
            var result = await _inventoryRepository.GetListAsync(this.GetCallerId());
            return Ok(new { inventories = result});
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetInventoriesByUserId([FromRoute] int userId)
        {
            var result = await _inventoryRepository.GetListAsync(userId);
            return Ok(new { inventories = result });
        }

        [HttpGet("currentUser/filter")]
        public async Task<IActionResult> GetCurrentUserFilteredInventories([FromQuery] bool? isActive = null)
        {
            var result = await _inventoryRepository.GetListAsync(this.GetCallerId(), isActive);

            return Ok(new { inventories = result });
        }

        [HttpGet("{$userId}/filter")]
        public async Task<IActionResult> GetFilteredInventoriesByUserId([FromRoute] int userId, [FromQuery] bool? isActive = null)
        {
            var result = await _inventoryRepository.GetListAsync(userId, isActive);

            return Ok(new { inventories = result });
        }

    }
}
