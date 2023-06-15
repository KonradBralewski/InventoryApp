namespace InventoryAppAPI.Models.Procedures
{
    public class ReportDetails
    {
        public int ReportNumber { get; set; }
        public string BuildingName { get; set; }
        public string RoomDescription { get; set; }
        public DateTime? InventoryEndedAt { get; set; }
    }
}
