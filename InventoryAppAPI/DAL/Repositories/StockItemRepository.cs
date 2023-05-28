using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly AppDbContext _dbContext;

        public StockItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<StockItems> GetByIdAsync(int id)
        {
            return await _dbContext.StockItems.FirstOrDefaultAsync(si => si.Id == id);
        }

        public async Task<IEnumerable<StockItems>> GetListAsync(Expression<Func<StockItems, bool>> predicate)
        {
            IQueryable<StockItems> stockItems = _dbContext.StockItems.Where(predicate);

            return await stockItems.ToListAsync();
        }

        public async Task<StockItems> AddAsync(AddStockItemRequest request)
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

            return stockItem;
        }
        public Task<StockItems> UpdateAsync(int id)
        {
            throw new NotImplementedException();
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
