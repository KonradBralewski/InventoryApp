using InventoryAppAPI.BLL.Services.Inventory;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Models.Requests.Procedures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        IInventoryRepository _inventoryRepository;
        IInventoryService _inventoryService;

        public InventoriesController(IInventoryRepository inventoryRepository, IInventoryService inventoryService)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryService = inventoryService;
        }

        [HttpPost("scan")]
        public async Task<IActionResult> ScanItemAsync([FromBody]ScanItemRequest request)
        {
            var result = await _inventoryService.ScanItem(request);
            return Ok(result);
        }

        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUserInventories()
        {
            var result = await _inventoryRepository.GetListAsync();
            return Ok(new {items = result});
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetInventoriesByUserId([FromRoute] int userId)
        {
            return Ok(await _inventoryRepository.GetListAsync());
        }

        [HttpGet("currentUser/filter")]
        public async Task<IActionResult> GetCurrentUserFilteredInventories([FromQuery] bool isActive)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{$userId}/filter")]
        public async Task<IActionResult> GetFilteredInventoriesByUserId([FromQuery] bool isActive)
        {
            throw new NotImplementedException();
        }

    }
}
