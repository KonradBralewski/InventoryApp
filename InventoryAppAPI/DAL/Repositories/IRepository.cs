using InventoryAppAPI.Models;

namespace InventoryAppAPI.DAL.Repositories
{
    public interface IRepository<T>
    {
        Task<dynamic> GetByIdAsync(Guid id);
        Task<dynamic> ListAsync();
        Task<Response> AddAsync(T dto);
        Task<Response> DeleteAsync(Guid id);
        Task<Response> UpdateAsync(T dto);
    }

}
