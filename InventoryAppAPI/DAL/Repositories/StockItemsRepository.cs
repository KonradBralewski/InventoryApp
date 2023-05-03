using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class StockItemsRepository : IRepository<StockItems>
    {
        private readonly AppDbContext _dbContext;

        public StockItemsRepository(AppDbContext dbContext)
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
        public async Task<StockItems> AddAsync(StockItems dto)
        {
            _dbContext.StockItems.Add(dto);
            await _dbContext.SaveChangesAsync();

            return dto;
        }
        public Task<StockItems> UpdateAsync(StockItems dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            StockItems stockItem = new StockItems() { Id = id };
            _dbContext.StockItems.Attach(stockItem);
            _dbContext.StockItems.Remove(stockItem);

            int result = await _dbContext.SaveChangesAsync();

            if(result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError,
                    "Could not delete stock item due an error. Please try again later.");
            }

            return true;
        }



    }
}
