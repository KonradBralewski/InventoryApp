using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Requests.Update;
using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly AppDbContext _dbContext;

        public InventoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<InventoryDTO> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<InventoryDTO>> GetListAsync()
        {
            return await _dbContext.InventoryView.ToListAsync();

        }

    }
}
