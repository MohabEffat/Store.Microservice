using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Orders.DataAccess.Contexts
{
    public static class SeedingContext
    {
        public static async Task ApplySeedingAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var context = services.GetRequiredService<AppDbContext>();
            try
            {
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
    
}
