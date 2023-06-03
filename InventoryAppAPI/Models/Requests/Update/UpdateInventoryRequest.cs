namespace InventoryAppAPI.Models.Requests.Update
{
    public class UpdateInventoryRequest
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }

        public string Description { get; set; }
    }
}
