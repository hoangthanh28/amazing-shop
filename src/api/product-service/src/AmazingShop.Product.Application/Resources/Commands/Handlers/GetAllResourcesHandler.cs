using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Application.Resource.Dto;
using AmazingShop.Shared.Core.Model;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Product.Application.Resource.Command.Handler
{
    public class GetAllResourcesHandler : IRequestHandler<GetAllResources, PagedList<GetAllResourceDto>>
    {
        private readonly IResourceRepository _resourceRepository;
        public GetAllResourcesHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }
        public async Task<PagedList<GetAllResourceDto>> Handle(GetAllResources request, CancellationToken cancellationToken)
        {
            var resources = await _resourceRepository.Resources.AsNoTracking().ToListAsync(cancellationToken);
            var result = resources.Select(GetAllResourceDto.Create);
            return PagedList<GetAllResourceDto>.Create(result);
        }
    }
}