using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _dbContext;

        public LocationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Location> GetByIdAsync(int id)
        {
            return await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Location>> GetListAsync(Expression<Func<Location, bool>> predicate)
        {
            IQueryable<Location> locations = _dbContext.Locations.Where(predicate);

            return await locations.ToListAsync();
        }

        public async Task<Location> AddAsync(Location dto)
        {
            _dbContext.Locations.Add(dto);
            await _dbContext.SaveChangesAsync();

            return dto;
        }
        public Task<Location> UpdateAsync(Location dto)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Location toBeDeleted = new Location { Id = id };

            _dbContext.Locations.Attach(toBeDeleted);
            _dbContext.Locations.Remove(toBeDeleted);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError,
                    "Could not delete location due an error. Please try again later.");
            }

            return true;
        }


    }
}
