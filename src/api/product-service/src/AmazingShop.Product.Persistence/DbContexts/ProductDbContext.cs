using AmazingShop.Product.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Product.Persistence.Context
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}