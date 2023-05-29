namespace InventoryAppAPI.Models.Requests.Update
{
    public class UpdateStockItemRequest
    {
        public string Code { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public bool IsArchive { get; set; }
    }
}
