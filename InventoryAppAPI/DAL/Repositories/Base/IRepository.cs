using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Base
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }

}
