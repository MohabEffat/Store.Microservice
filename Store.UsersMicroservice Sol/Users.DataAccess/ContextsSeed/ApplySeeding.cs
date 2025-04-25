using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Users.DataAccess.Contexts;
using Users.DataAccess.Entities;

namespace Users.DataAccess.ContextsSeed
{
    public static class ApplySeeding 
    {
        public static async Task ApplySeedingAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            var context = services.GetRequiredService<AppDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            try
            {
                await context.Database.MigrateAsync();
                await UserSeeding.SeedUserAsync(context, loggerFactory, userManager);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
