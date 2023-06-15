using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryAppAPI.DAL.Procedures
{
    public class GenerateReportProcedure
    {
        public string ProductName { get; set; }
        public string Code { get; set; }
        public int IsScanned { get; set; }
        public int IsInStock { get; set; }
        public int IsTarget { get; set; }
        public int IsArchived { get; set; }

        [NotMapped]
        public bool IsScannedBool { get { return IsScanned == 1; } }
        [NotMapped]
        public bool IsInStockBool { get { return IsInStock == 1; } }
        [NotMapped]
        public bool IsTargetBool { get { return IsTarget == 1; } }
        [NotMapped]
        public bool IsArchivedBool { get { return IsArchived == 1; } }
    }
}
