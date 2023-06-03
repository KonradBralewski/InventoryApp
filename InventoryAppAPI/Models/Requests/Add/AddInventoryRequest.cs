namespace InventoryAppAPI.Models.Requests.Add
{
    public class AddInventoryRequest
    {
        public int LocationId { get; set; }
        public int UserId { get; set; }

        public string Description { get; set; }
    }
}
