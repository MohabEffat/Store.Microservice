using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.BusinessLogic.RabbitMQ;
using Orders.BusinessLogic.Services;
using Orders.BusinessLogic.ServicesContracts;
using System.Reflection;

namespace Orders.BusinessLogic
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IOrderService, OrderService>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{configuration["Redis_Host"]}:{configuration["Redis_Port"]}";
                options.InstanceName = "Orders";
            });

            services.AddTransient<IProductNameUpdateConsumer, ProductNameUpdateConsumer>();
            services.AddHostedService<ProductNameUpdateHostedService>();
            services.AddTransient<IProductDeletionConsumer, ProductDeletionConsumer>();
            services.AddHostedService<ProductDeletionHostedService>();


            return services;
        }
    }
}
