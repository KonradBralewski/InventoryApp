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
        public async Task<RoomDTO> GetByIdAsync(int id)
        {
            Room found = await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == id);

            if(found == null)
            {
                return null;
            }

            return new RoomDTO(found);
        }

        public async Task<IEnumerable<RoomDTO>> GetListAsync(Expression<Func<Room, bool>> predicate)
        {
            IQueryable<Room> query = _dbContext.Rooms.Where(predicate);
            IEnumerable<RoomDTO> rooms = (await query.ToListAsync()).Select(r => new RoomDTO(r));

            return rooms;
        }

        public async Task<IEnumerable<RoomDTO>> GetListByBuildingIdAsync(int buildingId, Expression<Func<Room, bool>> predicate = default(Expression<Func<Room, bool>>))
        {
            IQueryable<Room> query = from location in _dbContext.Locations
                                                where buildingId == location.BuildingId
                                                join room in _dbContext.Rooms on location.RoomId equals room.Id
                                                select new Room
                                                {
                                                    Id = room.Id,
                                                    Name = room.Name,
                                                    CreatedAt = location.CreatedAt,
                                                    CreatedBy = location.CreatedBy,
                                                    ModifiedAt = location.ModifiedAt,
                                                    ModifiedBy = location.ModifiedBy
                                                };

            if(predicate != default(Expression<Func<Room, bool>>))
            {
                query = query.Where(predicate);
            }

            IEnumerable<RoomDTO> rooms = (await query.ToListAsync()).Select(r => new RoomDTO(r));

            return rooms;
        }
        public async Task<RoomDTO> AddRoomAsync(AddRoomRequest request)
        {
            Room room = new Room { Name = request.Name };

            _dbContext.Rooms.Add(room);
            await _dbContext.SaveChangesAsync();

            return new RoomDTO(room);
        }

        public async Task<RoomDTO> UpdateRoomAsync(int roomId, UpdateRoomRequest request)
        {
            Room room = await _dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);

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

            return new RoomDTO(room);
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
