using Microsoft.AspNetCore.Identity;

namespace Users.DataAccess.Entities
{
    public class AppUser : IdentityUser
    {
        public string? DisplayName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Address? Address { get; set; }
    }
}
