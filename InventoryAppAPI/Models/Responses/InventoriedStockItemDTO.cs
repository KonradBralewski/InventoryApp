namespace InventoryAppAPI.Models.Responses
{
    public class InventoriedStockItemDTO
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public int? ScannedItemEntryId { get; set; }
        public bool IsArchive { get; set; }
    }
}
