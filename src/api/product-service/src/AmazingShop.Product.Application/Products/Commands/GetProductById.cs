using AmazingShop.Product.Application.Product.Dto;
using MediatR;

namespace AmazingShop.Product.Application.Product.Command
{
    public class GetProductById : IRequest<ProductDetailDto>
    {
        public int Id { get; set; }
        public GetProductById(int id)
        {
            Id = id;
        }
    }
}