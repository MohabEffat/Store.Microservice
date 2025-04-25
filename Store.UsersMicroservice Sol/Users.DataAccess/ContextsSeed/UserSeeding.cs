using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Users.DataAccess.Contexts;
using Users.DataAccess.Data;
using Users.DataAccess.Entities;

namespace Users.DataAccess.ContextsSeed
{
    public class UserSeeding
    {
        public static async Task SeedUserAsync(AppDbContext context, ILoggerFactory loggerFactory,
            UserManager<AppUser> userManager)
        {
            try
            {
                if (!context.Roles.Any())
                {
                    var roles = new List<IdentityRole>
                        {
                            new IdentityRole { Name = UserRoles.Admin, NormalizedName = UserRoles.Admin.ToUpper() },
                            new IdentityRole { Name = UserRoles.User, NormalizedName = UserRoles.User.ToUpper() },
                            new IdentityRole { Name = UserRoles.Owner, NormalizedName = UserRoles.Owner.ToUpper() },
                            new IdentityRole { Name = UserRoles.Supplier, NormalizedName = UserRoles.Supplier.ToUpper() }
                        };
                    await context.Roles.AddRangeAsync(roles);
                    await context.SaveChangesAsync();

                }
                if (!context.Users.Any())
                {
                    var user = new AppUser
                    {
                        Email = "Admin@Info.com",
                        UserName = "Store_Admin",
                        DisplayName = "ADMIN",
                        Address = new Address
                        {
                            City = "Cairo",
                            State = "Info",
                            Street = "24",
                            PostalCode = "80100"
                        },
                    };
                    await userManager.CreateAsync(user, "Passw0rd@123");
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<AppDbContext>();
                logger.LogError(ex.Message);
            }
        }

    }
}
