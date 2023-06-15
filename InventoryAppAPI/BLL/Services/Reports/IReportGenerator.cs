using InventoryAppAPI.DAL.Procedures;
using InventoryAppAPI.Models.Procedures;

namespace InventoryAppAPI.BLL.Services.ReportGeneration
{
    public interface IReportGenerator
    {
        Task<PdfDocument> GenerateReportPDF(ReportDetails reportDetails, IEnumerable<GenerateReportProcedure> rawReports);
    }
}
