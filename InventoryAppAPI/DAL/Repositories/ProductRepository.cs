using InventoryAppAPI.DAL.Entities;
using InventoryAppAPI.DAL.Entities.Dicts;
using InventoryAppAPI.DAL.Repositories.Base;
using InventoryAppAPI.DAL.Repositories.Interfaces;
using InventoryAppAPI.Exceptions;
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
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetListAsync(Expression<Func<Product, bool>> predicate)
        {
            IQueryable<Product> products = _dbContext.Products.Where(predicate);

            return await products.ToListAsync();
        }

        public async Task<Product> AddAsync(Product dto)
        {
            _dbContext.Products.Add(dto);
            await _dbContext.SaveChangesAsync();

            return dto;
        }
        public Task<Product> UpdateAsync(Product dto)
        {
            throw new NotImplementedException();
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
