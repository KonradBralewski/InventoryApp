using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IBuildingRepository : IRepository<Building>
    {
        
        Task<IEnumerable<Building>> GetAllBuildingsAsync();
        Task<Building> AddBuildingAsync(AddBuildingRequest request);
        Task<Building> UpdateBuildingAsync(UpdateBuildingRequest request);
        
    }
}
