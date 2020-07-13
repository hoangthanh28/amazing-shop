using MediatR;
using AmazingShop.Shared.Core.Model;
using AmazingShop.Product.Application.Category.Dto;
namespace AmazingShop.Product.Application.Category.Command
{
    public class GetAllCategories : IRequest<PagedList<GetAllCategoriesDto>>
    {

    }
}