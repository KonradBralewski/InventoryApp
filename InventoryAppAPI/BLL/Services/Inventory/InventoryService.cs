using InventoryAppAPI.DAL;
using InventoryAppAPI.Models.Requests.Procedures;
using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace InventoryAppAPI.BLL.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public AppDbContext _dbContext { get; set; }

        public InventoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<dynamic> ScanItem(ScanItemRequest request)
        {
            try
            {
                var result = await _dbContext.Set<ScannedItemDTO>().FromSqlInterpolated($@"exec UP_APP_InventoryScanItemAdd 
                                {request.UserId}, {request.LocationId}, {request.Code}, {request.IsArchive}").ToListAsync();
                return result.First();
            }
            catch(Exception e)
            {
                return null;
            }

        }
    }
}
