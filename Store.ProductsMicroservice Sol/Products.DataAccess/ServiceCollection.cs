using Microsoft.Extensions.DependencyInjection;
using Products.DataAccess.Repositories;
using Products.DataAccess.RepositoriesContracts;

namespace Products.DataAccess
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }
    }
}
