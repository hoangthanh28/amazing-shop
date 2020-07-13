using System.Linq;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Domain.Entity;
using AmazingShop.Product.Persistence.Context;

namespace AmazingShop.Product.Persistence.Repository
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ProductDbContext _dbContext;
        public ResourceRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Resource> Resources => _dbContext.Resources;

        public IQueryable<Category> Categories => _dbContext.Categories;
    }
}