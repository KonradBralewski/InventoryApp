using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IStockItemRepository
    {
        Task<IEnumerable<InventoriedStockItemDTO>> GetListAsync(int locationId);
    }
}
