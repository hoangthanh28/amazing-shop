using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Product.Dto;
using AmazingShop.Product.Application.Repository.Abstraction;
using MediatR;

namespace AmazingShop.Product.Application.Product.Command.Handler
{
    public class UploadProductImageHandler : IRequestHandler<UploadProductImage, UploadProductImageDto>
    {
        private readonly IStorageService _storageService;
        public UploadProductImageHandler(IStorageService storageService)
        {
            _storageService = storageService;
        }
        public async Task<UploadProductImageDto> Handle(UploadProductImage request, CancellationToken cancellationToken)
        {
            // handle the upload image
            var path = await _storageService.UploadAsync(request.FileName, request.Content, request.ContentType);
            return UploadProductImageDto.Create(request.FileName, path, request.ContentType);
        }
    }
}