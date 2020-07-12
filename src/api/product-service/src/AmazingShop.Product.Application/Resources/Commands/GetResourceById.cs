using MediatR;
using AmazingShop.Product.Application.Resource.Dto;
namespace AmazingShop.Product.Application.Resource.Command
{
    public class GetResourceById : IRequest<GetResourceDetailDto>
    {
        public int Id { get; set; }
        public GetResourceById(int id)
        {
            Id = id;
        }
    }
}