using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
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
        private readonly IProductRepository _productRepository;
        private readonly ILocationRepository _locationRepository;

        public StockItemRepository(AppDbContext dbContext, ILocationRepository locationRepository, IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _locationRepository = locationRepository;
            _productRepository = productRepository;
        }
        public async Task<StockItemDTO> GetByIdAsync(int id)
        {
            StockItems found = await _dbContext.StockItems.FirstOrDefaultAsync(si => si.Id == id);

            if(found == null)
            {
                return null;
            }

            return new StockItemDTO(found);
        }

        public async Task<IEnumerable<StockItemDTO>> GetListAsync(Expression<Func<StockItems, bool>> predicate)
        {
            IQueryable<StockItems> query = _dbContext.StockItems.Where(predicate);
            IEnumerable<StockItemDTO> stockItems = (await query.ToListAsync()).Select(si => new StockItemDTO(si));

            return stockItems;
        }

        public async Task<StockItemDTO> AddStockItemAsync(AddStockItemRequest request)
        {
            StockItems stockItem = new StockItems
            {
                Code = request.Code,
                ProductId = request.ProductId,
                LocationId = request.LocationId,
                IsArchive = request.IsArchive
            };

            _dbContext.StockItems.Add(stockItem);

            await _dbContext.SaveChangesAsync();

            return new StockItemDTO(stockItem);
        }
        public async Task<StockItemDTO> UpdateAsync(int stockItemId, UpdateStockItemRequest request)
        {
            StockItems stockItem = await _dbContext.StockItems.FirstOrDefaultAsync(si => si.Id == stockItemId);

            if (stockItem == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given stockItemId could not be assosciated with any location.");
            }

            if (await _locationRepository.GetByIdAsync(request.LocationId) == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given location id could not be assosciated with any location.");
            }

            if (await _productRepository.GetByIdAsync(request.ProductId) == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given product id could not be assosciated with any product.");
            }

            if (request.ProductId == stockItem.ProductId
                && request.LocationId == stockItem.ProductId
                && request.Code == stockItem.Code)
            {
                throw new RequestException(StatusCodes.Status204NoContent, "Change request is the same as the resource. No changes were made.");
            }

            stockItem.ProductId = request.ProductId;
            stockItem.LocationId = request.LocationId;

            await _dbContext.SaveChangesAsync();

            return new StockItemDTO(stockItem);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            StockItems toBeDeleted = new StockItems { Id = id };

            _dbContext.StockItems.Attach(toBeDeleted);
            _dbContext.StockItems.Remove(toBeDeleted);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError,
                    "Could not delete product due an error. Please try again later.");
            }

            return true;
        }


    }
}
