using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        [HttpGet("currentUser")]
        public async Task<IActionResult> GetCurrentUserInventories()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{$userId}")]
        public async Task<IActionResult> GetInventoriesByUserId()
        {
            throw new NotImplementedException();
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
