using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<IEnumerable<LocationDTO>> GetAllLocationsByBuildingIdAsync(int buildingId);
        Task<Location> AddLocationAsync(AddLocationRequest request);
        Task<Location> UpdateLocationAsync(UpdateLocationRequest request);
    }
}
