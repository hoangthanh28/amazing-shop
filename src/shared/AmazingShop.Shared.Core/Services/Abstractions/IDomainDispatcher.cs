using System.Threading.Tasks;
using AmazingShop.Shared.Core.Event.Abstraction;

namespace AmazingShop.Shared.Core.Service.Dispatcher.Abstraction
{
    public interface IDomainDispatcher
    {
        Task DispatchEventAsync(IEvent evt);
    }
}