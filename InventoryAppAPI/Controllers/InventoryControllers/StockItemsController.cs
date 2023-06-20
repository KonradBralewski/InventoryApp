using InventoryAppAPI.Controllers.InventoryControllers.Abstract;
using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Requests.Update;
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

        [HttpDelete("{stockItemId}")]
        public async Task<IActionResult> DeleteStockItemById([FromRoute] int stockItemId)
        {
            return Ok(await _stockItemRepository.DeleteStockItemById(stockItemId));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStockItem([FromBody] AddStockItemRequest request)
        {
            return Ok(await _stockItemRepository.CreateStockItem(request));

        }

        [HttpPatch("{stockItemId}")]
        public async Task<IActionResult> UpdateStockItemById([FromRoute] int stockItemId, [FromBody] UpdateStockItemRequest request)
        {
            return Ok(await _stockItemRepository.UpdateStockItemAsync(stockItemId, request));
        }   
    }
}
