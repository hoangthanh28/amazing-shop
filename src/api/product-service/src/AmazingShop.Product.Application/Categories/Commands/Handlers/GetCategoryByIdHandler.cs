using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Application.Resource.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Product.Application.Category.Command.Handler
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, GetCategoryDetailDto>
    {
        private readonly IResourceRepository _resourceRepository;
        public GetCategoryByIdHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }
        public async Task<GetCategoryDetailDto> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.Categories.Include(x => x.Products).AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
            return GetCategoryDetailDto.Create(resource);
        }
    }
}