using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Order.Application.Order.Dto;
using AmazingShop.Order.Application.Repository.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Order.Application.Order.Command.Handler
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderById, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByIdHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<OrderDto> Handle(GetOrderById request, CancellationToken cancellationToken)
        {
            var entity = await _orderRepository.Orders.Where(x => x.Id == request.Id).Include(x => x.Details).AsNoTracking().FirstOrDefaultAsync();
            return OrderDto.Create(entity);
        }
    }
}