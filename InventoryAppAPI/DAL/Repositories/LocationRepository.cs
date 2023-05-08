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
        private readonly IRoomRepository _roomRepository;

        public LocationRepository(AppDbContext dbContext, IRoomRepository roomRepository = null, IBuildingRepository buildingRepository = null)
        {
            _dbContext = dbContext;
            _roomRepository = roomRepository;
            _buildingRepository = buildingRepository;
        }

        public async Task<IEnumerable<LocationDTO>> GetAllLocationsByBuildingIdAsync(int buildingId)
        {
            IQueryable<LocationDTO> locations = from location in _dbContext.Locations where buildingId == location.BuildingId
                                             join room in _dbContext.Rooms on location.RoomId equals room.Id
                                             select new LocationDTO
                                             {
                                                 Id = location.Id,
                                                 RoomId = location.RoomId,
                                                 Room = room,
                                                 CreatedAt = location.CreatedAt,
                                                 CreatedBy = location.CreatedBy,
                                                 ModifiedAt = location.ModifiedAt,
                                                 ModifiedBy = location.ModifiedBy
                                             };
    

            return await locations.ToListAsync();
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

        public async Task<Location> AddLocationAsync(AddLocationRequest request)
        {
            if(await _buildingRepository.GetByIdAsync(request.BuildingId) == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given building id could not be assosciated with any building.");
            }

            if (await _roomRepository.GetByIdAsync(request.RoomId) == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given room id could not be assosciated with any room.");
            }

            Location location = new Location { RoomId = request.RoomId, BuildingId = request.BuildingId};

            _dbContext.Locations.Add(location);
            await _dbContext.SaveChangesAsync();

            return location;
        }
        public async Task<Location> UpdateLocationAsync(UpdateLocationRequest request)
        {
            if (await _buildingRepository.GetByIdAsync(request.BuildingId) == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given building id could not be assosciated with any building.");
            }

            if (await _roomRepository.GetByIdAsync(request.RoomId) == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given room id could not be assosciated with any room.");
            }

            Location location = await GetByIdAsync(request.Id);

            if (location == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given location id could not be assosciated with any location.");
            }

            if (request.RoomId == location.RoomId && request.BuildingId == location.BuildingId)
            {
                throw new RequestException(StatusCodes.Status204NoContent, "Change request is the same as the resource. No changes were made.");
            }

            location.BuildingId = request.BuildingId;
            location.RoomId = request.RoomId;

            await _dbContext.SaveChangesAsync();

            return location;
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
