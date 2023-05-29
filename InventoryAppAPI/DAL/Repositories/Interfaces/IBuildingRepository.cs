using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IBuildingRepository : IRepository<BuildingDTO>
    {
        
        Task<IEnumerable<BuildingDTO>> GetAllBuildingsAsync();
        Task<BuildingDTO> AddBuildingAsync(AddBuildingRequest request);
        Task<BuildingDTO> UpdateBuildingAsync(int buildingId, UpdateBuildingRequest request);
        Task<IEnumerable<BuildingDTO>> GetListAsync(Expression<Func<Building, bool>> predicate);
    }
}
