using AmazingShop.Product.Application.Product.Dto;
using MediatR;

namespace AmazingShop.Product.Application.Product.Command
{
    public class UpdateProduct : IRequest<UpdateProductDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Domain.Entity.Product CreateEntity()
        {
            var entity = new Domain.Entity.Product();
            entity.Id = this.Id;
            entity.Name = this.Name;
            return entity;
        }
    }
}