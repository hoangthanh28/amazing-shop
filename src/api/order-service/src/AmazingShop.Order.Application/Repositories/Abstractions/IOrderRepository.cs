using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Order.Application.Repository.Abstraction
{
    public interface IOrderRepository
    {
        IQueryable<Domain.Entity.Order> Orders { get; }
        Task<Domain.Entity.Order> PlaceCustomerOrderAsync(Domain.Entity.Order order);
    }
}