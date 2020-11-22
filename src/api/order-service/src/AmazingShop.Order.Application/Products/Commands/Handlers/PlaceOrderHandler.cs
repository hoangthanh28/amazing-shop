using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Order.Application.Order.Dto;
using AmazingShop.Order.Application.Repository.Abstraction;
using MediatR;

namespace AmazingShop.Order.Application.Order.Command.Handler
{
    public class PlaceOrderHandler : IRequestHandler<PlaceOrder, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        public PlaceOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderDto> Handle(PlaceOrder request, CancellationToken cancellationToken)
        {
            var order = request.Create();
            var entity = await _orderRepository.PlaceCustomerOrderAsync(order);
            return OrderDto.Create(entity);
        }
    }
}