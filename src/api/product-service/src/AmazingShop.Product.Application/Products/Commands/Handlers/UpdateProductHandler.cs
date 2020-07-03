using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Product.Dto;
using AmazingShop.Product.Application.Repository.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var images = request.Images;
            var entity = await _productRepository.Products.Include(x => x.Images).SingleAsync(x => x.Id == request.Id);
            var existingImages = new System.Collections.Generic.HashSet<string>();
            foreach (var image in entity.Images)
            {
                if (!images.Contains(image.Url))
                {
                    image.Deleted = true;
                }
                existingImages.Add(image.Url);
            }
            foreach (var url in images)
            {
                if (!existingImages.Contains(url))
                {
                    entity.Images.Add(new Domain.Entity.ProductImage() { Product = product, Url = url });
                }
            }
            entity = await _productRepository.UpdateAsync(entity);
            return UpdateProductDto.Create(entity);
        }
    }
}