using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Users.BusinessLogic.Services;
using Users.BusinessLogic.ServicesContract;

namespace Users.BusinessLogic
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
            return services;
        }
    }
}
