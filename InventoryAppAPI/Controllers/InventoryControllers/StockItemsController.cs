using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace InventoryAppAPI.Controllers.InventoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin, User")]
    public class StockItemsController : ControllerBase
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockItemsController(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        [HttpGet("location/{locationId}")]
        public async Task<IActionResult> Get([FromRoute] int locationId)
        {
            return Ok(await _stockItemRepository.GetListAsync(si => si.LocationId == locationId));
        }
    }
}
