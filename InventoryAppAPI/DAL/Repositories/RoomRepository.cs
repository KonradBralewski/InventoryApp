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
    public class RoomRepository : IRoomRepository
    {
        private readonly AppDbContext _dbContext;

        public RoomRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Room> GetByIdAsync(int id)
        {
            return await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Room>> GetListAsync(Expression<Func<Room, bool>> predicate)
        {
            IQueryable<Room> rooms = _dbContext.Rooms.Where(predicate);

            return await rooms.ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetListByBuildingIdAsync(int buildingId, Expression<Func<Room, bool>> predicate = default(Expression<Func<Room, bool>>))
        {
            IQueryable<Room> rooms = from location in _dbContext.Locations
                                                where buildingId == location.BuildingId
                                                join room in _dbContext.Rooms on location.RoomId equals room.Id
                                                select new Room
                                                {
                                                    Id = location.Id,
                                                    Name = room.Name,
                                                    CreatedAt = location.CreatedAt,
                                                    CreatedBy = location.CreatedBy,
                                                    ModifiedAt = location.ModifiedAt,
                                                    ModifiedBy = location.ModifiedBy
                                                };

            if(predicate != default(Expression<Func<Room, bool>>))
            {
                rooms = rooms.Where(predicate);
            }

            return await rooms.ToListAsync();
        }
        public async Task<Room> AddRoomAsync(AddRoomRequest request)
        {
            Room room = new Room { Name = request.Name };

            _dbContext.Rooms.Add(room);
            await _dbContext.SaveChangesAsync();

            return room;
        }

        public async Task<Room> UpdateRoomAsync(UpdateRoomRequest request)
        {
            Room room = await GetByIdAsync(request.Id);

            if (room == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given id could not be assosciated with any room.");
            }

            if (request.Name == room.Name)
            {
                throw new RequestException(StatusCodes.Status204NoContent, "Change request is the same as the resource. No changes were made.");
            }

            room.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            return room;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Room toBeDeleted = new Room { Id = id };

            _dbContext.Attach(toBeDeleted);
            _dbContext.Remove(toBeDeleted);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError,
                    "Could not delete room due an error. Please try again later.");
            }

            return true;
        }


    }
}
