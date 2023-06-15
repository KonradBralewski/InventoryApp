using InventoryAppAPI.DAL;
using InventoryAppAPI.DAL.Views;
using InventoryAppAPI.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace InventoryAppAPI.BLL.Services.Reports
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext _dbContext;

        public ReportService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<dynamic> SaveReport(PdfDocument pdf, int inventoryId, int userId)
        {
            var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"INSERT INTO Reports
           (InventoryId
           ,CreatedAt
           ,Data
           ,CreatorId)
     VALUES
          ({inventoryId}, GETDATE(), {pdf.BinaryData}, {userId})");

            return result;
        }

        public async Task<ReportView> GetReportById(int id)
        {
            var found = await _dbContext.ReportsView.Where(r => r.Id == id).FirstOrDefaultAsync();

            if(found == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given id could not be assosciated with any report.");
            }

            return found;
        }

        public async Task<IEnumerable<ReportView>> GetReports()
        {
            var reports = await _dbContext.ReportsView.ToListAsync();

            return reports;
        }

        public async Task<byte[]> GetReportFileById(int id)
        {
            var found = await _dbContext.Files.Where(file => file.Id == id).FirstOrDefaultAsync();

            if (found == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given id could not be assosciated with any report.");
            }

            return found.Data;
        }

        public async Task<byte[]> GetLatestReportFile()
        {
            var last = await _dbContext.Files.OrderByDescending(f => f.Id).FirstOrDefaultAsync();

            if (last == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given id could not be assosciated with any report.");
            }

            return last.Data;
        }
    }
}
