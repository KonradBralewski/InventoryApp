using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Exceptions;
using InventoryAppAPI.Models.Requests.Add;
using InventoryAppAPI.Models.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryAppAPI.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            Product found = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if(found == null)
            {
                return null;
            }

            return new ProductDTO(found);
        }

        public async Task<IEnumerable<ProductDTO>> GetListAsync(Expression<Func<Product, bool>> predicate)
        {
            IQueryable<Product> query = _dbContext.Products.Where(predicate);
            IEnumerable<ProductDTO> products = (await query.ToListAsync()).Select(p => new ProductDTO(p));

            return products;
        }

        public async Task<ProductDTO> AddProductAsync(AddProductRequest request)
        {
            Product product = new Product { Name = request.Name };

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync();

            return new ProductDTO(product);
        }
        public async Task<ProductDTO> UpdateProductAsync(int productId, UpdateProductRequest request)
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new RequestException(StatusCodes.Status404NotFound, "Given id could not be assosciated with any product.");
            }

            if (request.Name == product.Name)
            {
                throw new RequestException(StatusCodes.Status204NoContent, "Change request is the same as the resource. No changes were made.");
            }

            product.Name = request.Name;

            await _dbContext.SaveChangesAsync();

            return new ProductDTO(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Product toBeDeleted = new Product { Id = id };

            _dbContext.Products.Attach(toBeDeleted);
            _dbContext.Products.Remove(toBeDeleted);

            int result = await _dbContext.SaveChangesAsync();

            if (result == 0)
            {
                throw new RequestException(StatusCodes.Status500InternalServerError,
                    "Could not delete product due an error. Please try again later.");
            }

            return true;
        }


    }
}
