using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> predicate);
        Task<bool> DeleteAsync(int id);
    }

}
