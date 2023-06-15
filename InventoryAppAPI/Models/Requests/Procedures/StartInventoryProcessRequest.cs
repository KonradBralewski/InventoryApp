namespace InventoryAppAPI.Models.Requests.Procedures
{
    public class StartInventoryProcessRequest
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; } = "Inwentaryzacja";
    }
}
