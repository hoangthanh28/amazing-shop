using MediatR;
using AmazingShop.Product.Application.Resource.Dto;
namespace AmazingShop.Product.Application.Category.Command
{
    public class GetCategoryById : IRequest<GetCategoryDetailDto>
    {
        public int Id { get; set; }
        public GetCategoryById(int id)
        {
            Id = id;
        }
    }
}