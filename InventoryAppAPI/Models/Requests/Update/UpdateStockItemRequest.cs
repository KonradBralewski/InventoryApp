namespace InventoryAppAPI.Models.Requests.Update
{
    public class UpdateStockItemRequest
    {
        public string? Code { get; set; } = null;
        public int? ProductId { get; set; } = null;
        public int? LocationId { get; set; } = null;
        public bool? IsArchive { get; set; } = null;
    }
}
