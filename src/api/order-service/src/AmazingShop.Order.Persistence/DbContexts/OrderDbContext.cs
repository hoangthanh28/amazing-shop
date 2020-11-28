using AmazingShop.Order.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Order.Persistence.Context
{
    public class OrderDbContext : DbContext
    {
        public DbSet<Domain.Entity.Order> Orders { get; set; }
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}