using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Product.Persistence.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _dbContext;
        public ProductRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Domain.Entity.Product> Products => _dbContext.Products;

        public async Task<Domain.Entity.Product> UpdateAsync(Domain.Entity.Product product)
        {
            _dbContext.Entry<Domain.Entity.Product>(product).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return product;
        }
    }
}