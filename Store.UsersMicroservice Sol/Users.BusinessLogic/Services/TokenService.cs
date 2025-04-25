using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Users.BusinessLogic.Options;
using Users.BusinessLogic.ServicesContract;
using Users.DataAccess.Entities;

namespace Users.BusinessLogic.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _options;
        private readonly UserManager<AppUser> _userManager;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IOptions<JwtOptions> options, UserManager<AppUser> userManager)
        {
            _options = options.Value;
            _userManager = userManager;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        }
        public async Task<string> GetTokenAsync(AppUser user)
        {
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
                new Claim(ClaimTypes.Role, role);

            var userCreds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = userCreds,
                Subject = new ClaimsIdentity(userClaims),
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Expires = DateTime.Now.AddDays(1)
            };


            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
