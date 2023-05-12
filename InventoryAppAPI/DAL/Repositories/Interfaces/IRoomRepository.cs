using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IRoomRepository : IRepository<Room>
    {
        Task<IEnumerable<Room>> GetListByBuildingIdAsync(int buildingId, Expression<Func<Room, bool>> predicate = default(Expression<Func<Room, bool>>));
        Task<Room> AddRoomAsync(AddRoomRequest request);
        Task<Room> UpdateRoomAsync(int roomId, UpdateRoomRequest request);
    }
}
