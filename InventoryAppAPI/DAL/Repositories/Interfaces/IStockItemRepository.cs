using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Views;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Requests.Update;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IStockItemRepository
    {
        Task<IEnumerable<InventoriedStockItemView>> GetListAsync(int locationId);
        Task<StockItem> CreateStockItem(AddStockItemRequest request);
        Task<bool> DeleteStockItemById(int stockItemId);
        Task<StockItem> UpdateStockItemAsync(int stockItemId, UpdateStockItemRequest request);
    }
}
