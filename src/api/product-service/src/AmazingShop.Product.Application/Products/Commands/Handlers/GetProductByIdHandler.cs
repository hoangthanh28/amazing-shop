using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Product.Dto;
using AmazingShop.Product.Application.Repository.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Product.Application.Product.Command.Handler
{
    public class GetProductByIdHandler : IRequestHandler<GetProductById, ProductDetailDto>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ProductDetailDto> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var entity = await _productRepository.Products.Where(x => x.Id == request.Id).Include(x => x.Images).AsNoTracking().FirstOrDefaultAsync();
            return ProductDetailDto.Create(entity);
        }
    }
}