using InventoryAppAPI.Controllers.InventoryControllers.Abstract;
using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace InventoryAppAPI.Controllers.InventoryControllers
{
    public class StockItemsController : InventoryAppController
    {
        private readonly IStockItemRepository _stockItemRepository;

        public StockItemsController(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        [HttpGet("location/{locationId}")]
        public async Task<IActionResult> Get([FromRoute] int locationId)
        {
            return Ok(await _stockItemRepository.GetListAsync(locationId));
        }
    }
}
