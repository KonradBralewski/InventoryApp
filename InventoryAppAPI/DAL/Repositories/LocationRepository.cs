using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IBuildingRepository _buildingRepository;

        public LocationRepository(AppDbContext dbContext, IBuildingRepository buildingRepository)
        {
            _dbContext = dbContext;
            _buildingRepository = buildingRepository;
        }
        public async Task<LocationDTO> GetByIdAsync(int id)
        {
            Location found = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == id);

            if (found == null)
            {
                return null;
            }

            return new LocationDTO(found);
        }

        public async Task<IEnumerable<LocationDTO>> GetListAsync(Expression<Func<Location, bool>> predicate)
        {
            IQueryable<Location> query = _dbContext.Locations.Where(predicate);
            IEnumerable<LocationDTO> locations = (await query.ToListAsync()).Select(l => new LocationDTO(l));

            return locations;
        }

        public async Task<LocationDTO> AddLocationAsync(AddLocationRequest request)
        {
            if(await _buildingRepository.GetByIdAsync(request.BuildingId) == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given building id could not be assosciated with any building.");
            }

            Location location = new Location
            {
                RoomNo = request.RoomId,
                BuildingId = request.BuildingId,
                RoomDescription = request.RoomDescription
            };

            _dbContext.Locations.Add(location);
            await _dbContext.SaveChangesAsync();

            return new LocationDTO(location);
        }
        public async Task<LocationDTO> UpdateLocationAsync(int locationId, UpdateLocationRequest request)
        {
            Location location = await _dbContext.Locations.FirstOrDefaultAsync(l => l.Id == locationId);

            if (location == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given location id could not be assosciated with any location.");
            }

            if (await _buildingRepository.GetByIdAsync(request.BuildingId) == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given building id could not be assosciated with any building.");
            }


            if (request.RoomId == location.RoomNo
                && request.BuildingId == location.BuildingId
                && request.RoomDescription == location.RoomDescription)
            {
                throw new RequestException(StatusCodes.Status204NoContent, "Change request is the same as the resource. No changes were made.");
            }

            location.BuildingId = request.BuildingId;
            location.RoomNo = request.RoomId;
            location.RoomDescription = request.RoomDescription;

            await _dbContext.SaveChangesAsync();

            return new LocationDTO(location);
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
