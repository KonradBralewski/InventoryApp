namespace InventoryAppAPI.Models.Requests.Update
{
    public class UpdateInventoryStatusRequest
    {
        public string StatusName { get; set; }
        public int InventoryId { get; set; }
        public bool isActive { get; set; }
    }
}
