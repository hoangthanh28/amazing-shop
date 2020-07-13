using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Application.Category.Dto;
using AmazingShop.Shared.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Product.Application.Category.Command.Handler
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategories, PagedList<GetAllCategoriesDto>>
    {
        private readonly IResourceRepository _resourceRepository;
        public GetAllCategoriesHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }
        public async Task<PagedList<GetAllCategoriesDto>> Handle(GetAllCategories request, CancellationToken cancellationToken)
        {
            var resources = await _resourceRepository.Categories.AsNoTracking().ToListAsync(cancellationToken);
            var result = resources.Select(GetAllCategoriesDto.Create);
            return PagedList<GetAllCategoriesDto>.Create(result);
        }
    }
}