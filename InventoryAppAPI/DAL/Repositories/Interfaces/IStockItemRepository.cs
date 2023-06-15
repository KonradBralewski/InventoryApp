using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Views;
using InventoryAppAPI.Models.Requests.Add;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IStockItemRepository
    {
        Task<IEnumerable<InventoriedStockItemView>> GetListAsync(int locationId);
    }
}
