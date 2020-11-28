using System.Linq;
using System.Threading.Tasks;
using AmazingShop.Order.Application.Repository.Abstraction;
using AmazingShop.Order.Persistence.Context;

namespace AmazingShop.Order.Persistence.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _dbContext;
        public OrderRepository(OrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Domain.Entity.Order> Orders => _dbContext.Orders;

        public async Task<Domain.Entity.Order> PlaceCustomerOrderAsync(Domain.Entity.Order order)
        {
            _dbContext.Add(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
    }
}