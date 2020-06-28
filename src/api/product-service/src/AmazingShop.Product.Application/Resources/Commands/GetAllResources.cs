using MediatR;
using AmazingShop.Shared.Core.Model;
using AmazingShop.Product.Application.Resource.Dto;
namespace AmazingShop.Product.Application.Resource.Command
{
    public class GetAllResources : IRequest<PagedList<GetAllResourceDto>>
    {

    }
}