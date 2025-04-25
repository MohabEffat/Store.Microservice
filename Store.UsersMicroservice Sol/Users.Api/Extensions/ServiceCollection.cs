using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Users.BusinessLogic.Options;
using Users.DataAccess.Contexts;
using Users.DataAccess.Entities;

namespace Users.Api.Extensions
{
    public static class ServiceCollection 
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {

            var connectionStringTemp = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("DefaultConnection not found in configuration");

            string GetEnv(string key) =>
            Environment.GetEnvironmentVariable(key)
            ?? throw new InvalidOperationException($"Environment variable '{key}' is not set.");

            var connectionString = connectionStringTemp
                .Replace("$SQL_HOST", GetEnv("SQL_HOST"))
                .Replace("$SQL_PORT", GetEnv("SQL_PORT"))
                .Replace("$SQL_DATABASE", GetEnv("SQL_DATABASE"))
                .Replace("$SQL_USER", GetEnv("SQL_USER"))
                .Replace("$SQL_PASSWORD", GetEnv("SQL_PASSWORD"));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });



            services.Configure<JwtOptions>(configuration.GetSection("JWT"));
            services.ConfigureOptions<JwtBearerOptionsSetup>(); 

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            //services.AddSingleton(provider =>
            //{
            //    var jwtOptions = provider.GetRequiredService<IOptions<JwtOptions>>().Value;
            //    var optionsSetup = new JwtBearerOptionsSetup(Options.Create(jwtOptions));
            //    return optionsSetup.CreateTokenValidationParameters();
            //});

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(jwtOptions =>
            //{
            //    using var scope = services.BuildServiceProvider().CreateScope();
            //    var tokenValidationParameters = scope.ServiceProvider.GetRequiredService<TokenValidationParameters>();
            //    jwtOptions.TokenValidationParameters = tokenValidationParameters;
            //});

            return services;
        }
    }
}
