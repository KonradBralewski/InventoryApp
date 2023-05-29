using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IRoomRepository : IRepository<RoomDTO>
    {
        Task<IEnumerable<RoomDTO>> GetListByBuildingIdAsync(int buildingId, Expression<Func<Room, bool>> predicate = default(Expression<Func<Room, bool>>));
        Task<RoomDTO> AddRoomAsync(AddRoomRequest request);
        Task<RoomDTO> UpdateRoomAsync(int roomId, UpdateRoomRequest request);
        Task<IEnumerable<RoomDTO>> GetListAsync(Expression<Func<Room, bool>> predicate);
    }
}
