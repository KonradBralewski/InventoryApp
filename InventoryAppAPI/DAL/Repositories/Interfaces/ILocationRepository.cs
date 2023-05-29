using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface ILocationRepository : IRepository<LocationDTO>
    {
        Task<IEnumerable<LocationDTO>> GetAllLocationsByBuildingIdAsync(int buildingId);
        Task<LocationDTO> AddLocationAsync(AddLocationRequest request);
        Task<LocationDTO> UpdateLocationAsync(int locationId, UpdateLocationRequest request);
        Task<IEnumerable<LocationDTO>> GetListAsync(Expression<Func<Location, bool>> predicate);
    }
}
