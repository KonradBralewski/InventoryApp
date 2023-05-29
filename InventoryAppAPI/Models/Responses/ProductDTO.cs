using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;

namespace InventoryAppAPI.Models.Responses
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ProductDTO(Product product)
        {
            Id = product.Id;
            Name = product.Name;
        }
    }
}
