using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AmazingShop.Product.Application.Extension
{
    public static class ApplicationExtension
    {
        public static void AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(ApplicationExtension).GetTypeInfo().Assembly);
        }
    }
}