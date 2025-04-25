using Microsoft.Extensions.DependencyInjection;
using Products.BusinessLogic.RabbitMQ;
using Products.BusinessLogic.Services;
using Products.BusinessLogic.ServicesContracts;
using System.Reflection;

namespace Products.BusinessLogic
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            //services.AddHttpClient<UserMicroserviceClient>(c =>
            //{
            //    c.BaseAddress = new Uri("https://localhost:7232");
            //});

            //services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();
            return services;
        }
    }
}
