using AmazingShop.Order.Application.Order.Dto;
using MediatR;

namespace AmazingShop.Order.Application.Order.Command
{
    public class GetOrderById : IRequest<OrderDto>
    {
        public int Id { get; set; }
        public GetOrderById(int id)
        {
            Id = id;
        }
    }
}