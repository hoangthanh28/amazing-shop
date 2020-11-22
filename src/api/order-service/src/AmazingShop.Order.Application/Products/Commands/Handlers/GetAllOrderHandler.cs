using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Order.Application.Order.Dto;
using AmazingShop.Order.Application.Repository.Abstraction;
using AmazingShop.Shared.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Order.Application.Order.Command.Handler
{
    public class GetAllOrderHandler : IRequestHandler<GetAllOrder, PagedList<OrderDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<PagedList<OrderDto>> Handle(GetAllOrder request, CancellationToken cancellationToken)
        {
            var entities = await _orderRepository.Orders.AsNoTracking().ToListAsync();
            var data = entities.Select(OrderDto.Create);
            return PagedList<OrderDto>.Create(data);
        }
    }
}