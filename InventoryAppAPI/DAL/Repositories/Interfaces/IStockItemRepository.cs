using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IStockItemRepository : IRepository<StockItemDTO>
    {
        Task<StockItemDTO> AddStockItemAsync(AddStockItemRequest request);
        Task<IEnumerable<StockItemDTO>> GetListAsync(Expression<Func<StockItems, bool>> predicate);
    }
}
