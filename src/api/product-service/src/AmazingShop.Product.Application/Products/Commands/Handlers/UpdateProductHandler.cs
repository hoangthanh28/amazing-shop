using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Product.Dto;
using AmazingShop.Product.Application.Repository.Abstraction;
using MediatR;

namespace AmazingShop.Product.Application.Product.Command.Handler
{
    public class UpdateProductHandler : IRequestHandler<UpdateProduct, UpdateProductDto>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<UpdateProductDto> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var product = request.CreateEntity();
            var entity = await _productRepository.UpdateAsync(product);
            return UpdateProductDto.Create(entity);
        }
    }
}