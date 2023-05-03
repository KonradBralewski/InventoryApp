using InventoryAppAPI.DAL.Entities;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<StockItems> GetByIdAsync(int id);
        Task<IEnumerable<StockItems>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<StockItems> AddAsync(T dto);
        Task<StockItems> UpdateAsync(T dto);
        Task<bool> DeleteAsync(int id);
    }

}
