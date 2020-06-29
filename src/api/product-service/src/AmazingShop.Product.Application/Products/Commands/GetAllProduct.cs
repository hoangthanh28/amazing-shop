using AmazingShop.Product.Application.Product.Dto;
using AmazingShop.Shared.Core.Model;
using MediatR;

namespace AmazingShop.Product.Application.Product.Command
{
    public class GetAllProduct : IRequest<PagedList<ProductDetailDto>>
    {
    }
}