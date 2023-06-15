using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.DAL.Views;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Requests.Update;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly AppDbContext _dbContext;

        public StockItemRepository(AppDbContext dbContext, ILocationRepository locationRepository, IProductRepository productRepository)
        {
            _dbContext = dbContext;
        }
 

        public async Task<IEnumerable<InventoriedStockItemView>> GetListAsync(int locationId)
        {
            IQueryable<InventoriedStockItemView> inventoriedStockItems = _dbContext.InventoriedStockItemsView.Where(si => si.LocationId == locationId);

            return await inventoriedStockItems.ToListAsync();
        }

    }
}
