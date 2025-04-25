using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Products.DataAccess.Contexts;
using System.Text;

namespace Products.Api.Extensions
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services,
            IConfiguration configuration)
        {
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

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!))
                };
            });

            return services;
        }
    }
}
