namespace InventoryAppAPI.DAL.Views
{
    public class ReportView
    {
        public int Id { get; set; }
        public int InventoryId { get; set; }
        public string BuildingName { get; set; }

        public string RoomDescription { get; set; }
        public DateTime ReportDate { get; set; }
    }
}
