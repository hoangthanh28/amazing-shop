using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Product.Dto;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Shared.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Product.Application.Product.Command.Handler
{
    public class GetAllProductHandler : IRequestHandler<GetAllProduct, PagedList<ProductDetailDto>>
    {
        private readonly IProductRepository _productRepository;
        public GetAllProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<PagedList<ProductDetailDto>> Handle(GetAllProduct request, CancellationToken cancellationToken)
        {
            var entities = await _productRepository.Products.AsNoTracking().ToListAsync();
            var data = entities.Select(ProductDetailDto.Create);
            return PagedList<ProductDetailDto>.Create(data);
        }
    }
}