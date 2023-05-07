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

        public async Task<Room> AddAsync(Room dto)
        {
            _dbContext.Add(dto);
            await _dbContext.SaveChangesAsync();

            return dto;
        }
        public Task<Room> UpdateAsync(int id)
        {
            throw new NotImplementedException();
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
