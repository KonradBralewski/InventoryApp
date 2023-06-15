using InventoryAppAPI.DAL;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Views;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Procedures;
using InventoryAppAPI.DAL.Procedures;
using Microsoft.EntityFrameworkCore;
using InventoryAppAPI.BLL.Services.ReportGeneration;
using InventoryAppAPI.Models.Procedures;
using IronPdf;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
using InventoryAppAPI.BLL.Services.Reports;

namespace InventoryAppAPI.BLL.Services.Inventory
{
    public class InventoryService : IInventoryService
    {
        public AppDbContext _dbContext { get; set; }
        private IReportGenerator _reportGenerator { get; set; }
        private IHttpContextAccessor _httpContextAccessor { get; set; }

        private IReportService _reportService { get; set; }

        public InventoryService(AppDbContext dbContext, IReportGenerator reportGenerator, IHttpContextAccessor httpContextAccessor, IReportService reportService)
        {
            _dbContext = dbContext;
            _reportGenerator = reportGenerator;
            _httpContextAccessor = httpContextAccessor;
            _reportService = reportService;
        }

        public async Task<dynamic> StartInventoryProcess(StartInventoryProcessRequest request)
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"exec UP_APP_InventoryAdd 
                                {request.LocationId}, {request.UserId}, {request.Description}");
                return result;
            }
            catch (Exception e)
            {
                throw new RequestException(StatusCodes.Status400BadRequest, e.Message);
            }
        }

        public async Task<dynamic> EndInventoryProcess(EndInventoryProcessRequest request)
        {
            IEnumerable<InventoryView> activeInventories = await _dbContext.InventoryView.Where(inv => inv.IsActive && inv.LocationId == request.LocationId && inv.UserId == request.UserId).ToListAsync();

            InventoryView activeInventory = activeInventories.FirstOrDefault(); // Domain should allow only one active inventory per user

            if (activeInventory == null)
            {
                throw new RequestException(StatusCodes.Status400BadRequest, "Próba zakończenia inwentaryzacji mimo że żadna aktywna nie istnieje.");
            }

            try
            {
                var statusSetResult = await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"exec UP_APP_InventoryStatusSet 
                                {activeInventory.InventoryId}, {2}, {request.UserId}"); // magic number 2 means "ended" - awful but there's no time

                IEnumerable<GenerateReportProcedure> generateRaport = await _dbContext.RawReports.FromSqlRaw($@"exec UP_APP_GenerateRap {activeInventory.InventoryId}").ToListAsync();
                PdfDocument pdf = await _reportGenerator.GenerateReportPDF(new ReportDetails
                {
                    ReportNumber = _dbContext.Reports.Count() + 1,
                    BuildingName = activeInventory.BuildingName,
                    RoomDescription = activeInventory.RoomDescription,
                    InventoryEndedAt = DateTime.Now
                }, generateRaport);

                await _reportService.SaveReport(pdf, activeInventory.InventoryId, request.UserId);

                var cleanupScannedItemsTableResult = await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"exec UP_APP_CleanupScannedItems");

                return true;

            }
            catch (Exception e)
            {
                throw new RequestException(StatusCodes.Status400BadRequest, e.Message);
            }
        }
        public async Task<dynamic> ScanItem(ScanItemRequest request)
        {
            try
            {
                var result = await _dbContext.Database.ExecuteSqlInterpolatedAsync($@"exec UP_APP_InventoryScanItemAdd 
                                {request.UserId}, {request.LocationId}, {request.Code}, {request.IsArchive}");
                return result;
            }
            catch(Exception e)
            {
                throw new RequestException(StatusCodes.Status400BadRequest, e.Message);
            }

        }
    }
}
