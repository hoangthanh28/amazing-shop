using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Event;
using AmazingShop.Product.Application.Product.Dto;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Application.Service.Dispatcher.Abstraction;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace AmazingShop.Product.Application.Product.Command.Handler
{
    public class UpdateProductHandler : IRequestHandler<UpdateProduct, UpdateProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IDomainDispatcher _domainDispatcher;
        public UpdateProductHandler(IProductRepository productRepository, IDomainDispatcher domainDispatcher)
        {
            _productRepository = productRepository;
            _domainDispatcher = domainDispatcher;
        }
        public async Task<UpdateProductDto> Handle(UpdateProduct request, CancellationToken cancellationToken)
        {
            var product = request.CreateEntity();
            var images = request.Images.Select(x => x.Url);
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
                var image = request.Images.First(x => x.Url == url);
                if (!existingImages.Contains(url))
                {
                    entity.Images.Add(new Domain.Entity.ProductImage() { Product = product, Url = url, Name = image.Name, ContentType = image.ContentType });
                }
            }
            entity = await _productRepository.UpdateAsync(entity);
            var tasks = request.Images.Select(x => _domainDispatcher.DispatchEventAsync(new ImageAddedEvent(request.Id, "images", x.Name, x.ContentType)));
            await Task.WhenAll(tasks);
            return UpdateProductDto.Create(entity);
        }
    }
}