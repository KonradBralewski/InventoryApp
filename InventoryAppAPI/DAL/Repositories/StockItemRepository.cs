using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.DAL.Views;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Requests.Update;
using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly AppDbContext _dbContext;

        public StockItemRepository(AppDbContext dbContext, ILocationRepository locationRepository, IProductRepository productRepository)
        {
            _dbContext = dbContext;
        }
 

        public async Task<IEnumerable<InventoriedStockItemView>> GetListAsync(int locationId)
        {
            IQueryable<InventoriedStockItemView> inventoriedStockItems = _dbContext.InventoriedStockItemsView.Where(si => si.LocationId == locationId);

            return await inventoriedStockItems.ToListAsync();
        }

        public async Task<StockItem> CreateStockItem(AddStockItemRequest request)
        {
            StockItem stockItem = new StockItem
            {
                LocationId = request.LocationId,
                ProductId = request.ProductId,
                Code = request.Code,
                IsArchive = request.IsArchive
            };
            _dbContext.StockItems.Add(stockItem);

            await _dbContext.SaveChangesAsync();

            return stockItem;
        }
        public async Task<bool> DeleteStockItemById(int stockItemId)
        {
            StockItem itemToDelete = await _dbContext.StockItems.FirstOrDefaultAsync(si => si.Id == stockItemId);

            if(itemToDelete == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound,
                                       "Could not find stock item with id " + stockItemId + ". Please try again later.");
            }
            _dbContext.StockItems.Attach(itemToDelete);
            _dbContext.StockItems.Remove(itemToDelete);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError,
                    "Could not delete stock item due an error. Please try again later.");
            }

            return true;
        }

        public async Task<StockItem> UpdateStockItemAsync(int stockItemId, UpdateStockItemRequest request)
        {
            StockItem stockItem = await _dbContext.StockItems.FirstOrDefaultAsync(si => si.Id == stockItemId);

            if (stockItem == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given id could not be assosciated with any stock item.");
            }

            if (request.Code == stockItem.Code && request.IsArchive == stockItem.IsArchive && request.LocationId == stockItem.LocationId && request.ProductId == stockItem.ProductId)
            {
                throw new RequestException(StatusCodes.Status204NoContent, "Change request is the same as the resource. No changes were made.");
            }

            if (request.Code != null) { stockItem.Code = (string)request.Code; }
            if(request.IsArchive != null) { stockItem.IsArchive = (bool)request.IsArchive; }
            if(request.LocationId != null) { stockItem.LocationId = (int)request.LocationId; }
            if(request.ProductId != null) { stockItem.ProductId = (int)request.ProductId; }
     

            await _dbContext.SaveChangesAsync();

            return stockItem;
        }

    }
}
