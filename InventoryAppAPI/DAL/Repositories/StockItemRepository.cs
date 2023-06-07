using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Requests.Update;
using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IProductRepository _productRepository;
        private readonly ILocationRepository _locationRepository;

        public StockItemRepository(AppDbContext dbContext, ILocationRepository locationRepository, IProductRepository productRepository)
        {
            _dbContext = dbContext;
            _locationRepository = locationRepository;
            _productRepository = productRepository;
        }
 

        public async Task<IEnumerable<InventoriedStockItemDTO>> GetListAsync(int locationId)
        {
            IEnumerable<InventoriedStockItemDTO> inventoriedStockItems = await _dbContext.InventoriedStockItemsView.ToListAsync();

            return inventoriedStockItems.Where(si => si.LocationId == locationId);
        }

    }
}
