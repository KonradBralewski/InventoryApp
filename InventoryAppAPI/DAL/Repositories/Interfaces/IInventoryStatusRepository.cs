using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Requests.Update;
using InventoryAppAPI.Models.Responses;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IInventoryStatusRepository : IRepository<InventoryStatus> 
    {
        Task<InventoryStatusDTO> AddInventoryStatusAsync(AddInventoryStatusRequest request);
        Task<InventoryStatusDTO> UpdateInventoryStatusAsync(int inventoryStatusId, UpdateInventoryStatusRequest request);
        Task<IEnumerable<InventoryStatusDTO>> GetListAsync(Expression<Func<InventoryStatus, bool>> predicate);
    }
}
