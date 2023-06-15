using InventoryAppAPI.BLL.Services.Reports;
using InventoryAppAPI.Controllers.InventoryControllers.Abstract;
using InventoryAppAPI.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InventoryAppAPI.Controllers.InventoryControllers
{
    public class ReportsController : InventoryAppController
    {
        private readonly IReportService _reportService;
        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            return Ok(await _reportService.GetReports());
        }

        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetReportById([FromRoute] int reportId)
        {
            return Ok(await _reportService.GetReportById(reportId));
        }

        [HttpGet("file/{reportId}")]
        public async Task<IActionResult> GetReportFileById([FromRoute] int reportId)
        {
            var file = await _reportService.GetReportFileById(reportId);

            return new FileStreamResult(new MemoryStream(file), "application/pdf")
            {
                FileDownloadName = "test.pdf"
            };
        }

        [HttpGet("file/latest")]
        public async Task<IActionResult> GetLatestReportFile()
        {
            var file = await _reportService.GetLatestReportFile();

            return new FileStreamResult(new MemoryStream(file), "application/pdf")
            {
                FileDownloadName = "test.pdf"
            };
        }
    }
}
