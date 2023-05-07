using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Responses;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface ILocationRepository : IRepository<Location>
    {
        public Task<IEnumerable<LocationDTO>> GetAllLocationsByBuildingIdAsync(int buildingId);
    }
}
