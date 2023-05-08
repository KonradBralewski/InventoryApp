using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly AppDbContext _dbContext;

        public BuildingRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Building>> GetAllBuildingsAsync()
        {
            return await _dbContext.Buildings.ToListAsync();
        }
        public async Task<Building> GetByIdAsync(int id)
        {
            return await _dbContext.Buildings.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Building>> GetListAsync(Expression<Func<Building, bool>> predicate)
        {
            IQueryable<Building> buildings = _dbContext.Buildings.Where(predicate);

            return await buildings.ToListAsync();
        }

        public async Task<Building> AddBuildingAsync(AddBuildingRequest request)
        {
            Building building = new Building { Name = request.Name };

            _dbContext.Buildings.Add(building);
            await _dbContext.SaveChangesAsync();

            return building;
        }
        public async Task<Building> UpdateBuildingAsync(UpdateBuildingRequest request)
        {
            Building building = await GetByIdAsync(request.Id);

            if(building == null) 
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given id could not be assosciated with any building.");
            }

            if(request.Name == building.Name)
            {
                throw new RequestException(StatusCodes.Status204NoContent, "Change request is the same as the resource. No changes were made.");
            }

            building.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            return building;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Building toBeDeleted = new Building { Id = id };

            _dbContext.Buildings.Attach(toBeDeleted);
            _dbContext.Buildings.Remove(toBeDeleted);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError,
                    "Could not delete building due an error. Please try again later.");
            }

            return true;
        }

    }
}
