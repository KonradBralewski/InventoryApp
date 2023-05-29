using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<ProductDTO>
    {
        Task<IEnumerable<ProductDTO>> GetListAsync(Expression<Func<Product, bool>> predicate);
        Task<ProductDTO> AddProductAsync(AddProductRequest request);
        Task<ProductDTO> UpdateProductAsync(int roomId, UpdateProductRequest request);
    }
}
