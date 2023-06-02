namespace InventoryAppAPI.Models.Requests.Add
{
    public class AddInventoryStatusRequest
    {
        public string StatusName { get; set; }
        public int InventoryId { get; set; }
        public bool isActive { get; set; }

    }
}
