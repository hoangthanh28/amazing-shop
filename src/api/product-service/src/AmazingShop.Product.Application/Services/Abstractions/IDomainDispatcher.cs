using System.Threading.Tasks;
using AmazingShop.Product.Application.Event.Abstraction;

namespace AmazingShop.Product.Application.Service.Dispatcher.Abstraction
{
    public interface IDomainDispatcher
    {
        Task DispatchEventAsync(IEvent evt);
    }
}