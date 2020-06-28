using AmazingShop.Product.Persistence.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using AmazingShop.Product.Application.Repository.Abstraction;
using AmazingShop.Product.Persistence.Repository;

namespace AmazingShop.Product.Persistence.Extension
{
    public static class PersistenceExtensions
    {
        public static void AddPersistenceServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContextPool<ProductDbContext>((service, option) =>
            {
                var configuration = service.GetService<IConfiguration>();
                var productConnectionString = configuration.GetConnectionString("Product");
                option.UseSqlServer(productConnectionString);
            });
            serviceCollection.AddScoped<IResourceRepository, ResourceRepository>();
        }
    }
}