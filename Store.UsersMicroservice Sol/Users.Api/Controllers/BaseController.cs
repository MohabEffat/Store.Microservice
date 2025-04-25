using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Users.BusinessLogic.ServicesContract;
using Users.DataAccess.Entities;

namespace Users.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ITokenService _tokenService;
        protected readonly UserManager<AppUser> _userManager;
        protected readonly SignInManager<AppUser> _signInManager;
        protected readonly RoleManager<IdentityRole> _roleManager;
        public BaseController(ITokenService tokenService,
            UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
    }
}
