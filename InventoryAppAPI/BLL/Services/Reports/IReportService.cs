using InventoryAppAPI.DAL.Views;

namespace InventoryAppAPI.BLL.Services.Reports
{
    public interface IReportService
    {
        Task<dynamic> SaveReport(PdfDocument pdf, int inventoryId, int userId);
        Task<ReportView> GetReportById(int id);
        Task<byte[]> GetReportFileById(int id);
        Task<byte[]> GetLatestReportFile();
        Task<IEnumerable<ReportView>> GetReports();
    }
}
