using AmazingShop.Order.Application.Order.Dto;
using AmazingShop.Shared.Core.Model;
using MediatR;

namespace AmazingShop.Order.Application.Order.Command
{
    public class GetAllOrder : IRequest<PagedList<OrderDto>>
    {
    }
}