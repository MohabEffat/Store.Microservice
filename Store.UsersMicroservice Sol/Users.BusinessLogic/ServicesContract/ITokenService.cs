using Users.DataAccess.Entities;

namespace Users.BusinessLogic.ServicesContract
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync(AppUser user);
    }
}
