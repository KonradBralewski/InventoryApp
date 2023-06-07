using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Responses
{
    public class StockItemDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public ProductDTO Product { get; set; }
        public int LocationId { get; set; }
        public bool IsArchive { get; set; }

        public StockItemDTO(StockItem stockItem, Product product)
        {
            Id = stockItem.Id;
            Code = stockItem.Code;
            Product = new ProductDTO(product);
            LocationId = stockItem.LocationId;
            IsArchive = stockItem.IsArchive;
        }
    }
}
