namespace InventoryAppAPI.Models.Requests.Procedures
{
    public class EndInventoryProcessRequest
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }
    }
}
