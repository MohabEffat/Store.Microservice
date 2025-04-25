using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Orders.DataAccess.Contexts;
using Orders.DataAccess.Repositories;
using Orders.DataAccess.RepositoriesContracts;

namespace Orders.DataAccess
{
    public static class ServicesCollection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services,
            IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("MongoDB");
            //var databaseName = configuration["MongoDB:DatabaseName"];

            //services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
            //services.AddSingleton(provider =>
            //{
            //    var client = provider.GetRequiredService<IMongoClient>();
            //    return client.GetDatabase(databaseName);
            //});

            var connectionStringTemp = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection not found in configuration");

            var connectionString = connectionStringTemp
                .Replace("$SQL_HOST", Environment.GetEnvironmentVariable("SQL_HOST"))
                .Replace("$SQL_PORT", Environment.GetEnvironmentVariable("SQL_PORT"))
                .Replace("$SQL_DATABASE", Environment.GetEnvironmentVariable("SQL_DATABASE"))
                .Replace("$SQL_USER", Environment.GetEnvironmentVariable("SQL_USER"))
                .Replace("$SQL_PASSWORD", Environment.GetEnvironmentVariable("SQL_PASSWORD"));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            return services;
        }
    }
}
