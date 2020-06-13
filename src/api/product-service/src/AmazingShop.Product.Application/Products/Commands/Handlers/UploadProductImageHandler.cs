using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Event;
using AmazingShop.Product.Application.Product.Dto;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Application.Service.Dispatcher.Abstraction;
using MediatR;

namespace AmazingShop.Product.Application.Product.Command.Handler
{
    public class UploadProductImageHandler : IRequestHandler<UploadProductImage, UploadProductImageDto>
    {
        private readonly IStorageService _storageService;
        private readonly IDomainDispatcher _domainDispatcher;
        public UploadProductImageHandler(IStorageService storageService, IDomainDispatcher domainDispatcher)
        {
            _storageService = storageService;
            _domainDispatcher = domainDispatcher;
        }
        public async Task<UploadProductImageDto> Handle(UploadProductImage request, CancellationToken cancellationToken)
        {
            // handle the upload image
            var path = await _storageService.UploadAsync(request.FileName, request.Content, request.ContentType);
            await _domainDispatcher.DispatchEventAsync(new ImageAddedEvent(request.OriginalFileName, path, request.ContentType));
            return UploadProductImageDto.Create(path);
        }
    }
}