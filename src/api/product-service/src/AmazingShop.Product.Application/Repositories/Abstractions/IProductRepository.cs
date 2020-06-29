using System.Linq;
using System.Threading.Tasks;

namespace AmazingShop.Product.Application.Repository.Abstraction
{
    public interface IProductRepository
    {
        IQueryable<Domain.Entity.Product> Products { get; }
        Task<Domain.Entity.Product> UpdateAsync(Domain.Entity.Product product);
    }
}