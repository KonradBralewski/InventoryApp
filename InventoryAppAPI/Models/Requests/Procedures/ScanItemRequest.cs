namespace InventoryAppAPI.Models.Requests.Procedures
{
    public class ScanItemRequest
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public string Code { get; set; }
        public bool IsArchive { get; set; }
    }
}
