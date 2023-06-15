using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.DAL.Views;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Requests.Update;
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

        public async Task<IEnumerable<InventoryView>> GetListAsync(int userId, bool? isActive = null, int? locationId = null)
        {
            IQueryable<InventoryView> inventoriesQuery = _dbContext.InventoryView.Where(inv => inv.UserId == userId);


            if (locationId != null)
            {
                inventoriesQuery = inventoriesQuery.Where(inv => inv.LocationId == locationId);
            }

            if (isActive != null)
            {
                inventoriesQuery = inventoriesQuery.Where(inv => inv.IsActive == isActive);
            }

            return await inventoriesQuery.ToListAsync();
        }

    }
}
