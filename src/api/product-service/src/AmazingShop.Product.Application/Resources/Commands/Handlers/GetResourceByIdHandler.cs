using System.Threading;
using System.Threading.Tasks;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Application.Resource.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AmazingShop.Product.Application.Resource.Command.Handler
{
    public class GetResourceByIdHandler : IRequestHandler<GetResourceById, GetResourceDetailDto>
    {
        private readonly IResourceRepository _resourceRepository;
        public GetResourceByIdHandler(IResourceRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }
        public async Task<GetResourceDetailDto> Handle(GetResourceById request, CancellationToken cancellationToken)
        {
            var resource = await _resourceRepository.Resources.Include(x => x.Categories).AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
            return GetResourceDetailDto.Create(resource);
        }
    }
}