using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Users.BusinessLogic.Dtos;
using Users.BusinessLogic.ServicesContract;
using Users.DataAccess.Data;
using Users.DataAccess.Entities;

namespace Users.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        public UserController(ITokenService tokenService,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
            : base(tokenService, userManager, signInManager, roleManager)
        {
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user == null)
                return NotFound("User Not Found.");
            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);

            if (!result.Succeeded)
                return NotFound("Invalid Credentials.");
            var response = new ResponseDto
            {
                UserId = Guid.Parse(user.Id),
                Email = user.Email!,
                Token = await _tokenService.GetTokenAsync(user),

            };
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user != null)
                return BadRequest("Email Already Registered");

            var appUser = new AppUser
            {
                Email = input.Email,
                UserName = $"{input.FirstName}_{input.LastName}",
                DateOfBirth = input.DateOfBirth,
                DisplayName = $"{input.FirstName}_User",
                Address = new Address
                {
                    City = input.City,
                    PostalCode = input.PostalCode,
                    State = input.State,
                    Street = input.Street
                }
            };

            if (input.Password != input.ConfirmedPassword)
                return BadRequest("Passwords Do Not Match");

            var result = await _userManager.CreateAsync(appUser, input.Password);

            await _userManager.AddToRoleAsync(appUser, UserRoles.User);

            if (!result.Succeeded)
                return BadRequest(result.Errors.Select(x => x.Description));

            var response = new ResponseDto
            {
                UserId = Guid.Parse(appUser.Id),
                Email = input.Email,
                Token = await _tokenService.GetTokenAsync(appUser),
            };

            return Ok(response);
        }

        [HttpGet("{userId}")]
        [Authorize()]
        public async Task<IActionResult> GetUserById (Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return BadRequest("User Not Found");
            var existingUser = new UserDto
            {
                UserId = Guid.Parse(user.Id),
                UserName = user.UserName!,
                Email = user.Email!,
                Token = await _tokenService.GetTokenAsync(user),
                UserRoles = await _userManager.GetRolesAsync(user)
            };
            return Ok(existingUser);
        }
        [HttpGet("Email/{Email}")]
        public async Task<IActionResult> GetUserByEmail(string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null) return BadRequest("User Not Found");
            var existingUser = new UserDto
            {
                UserId = Guid.Parse(user.Id),
                UserName = user.UserName!,
                Email = user.Email!,
                UserRoles = await _userManager.GetRolesAsync(user)
            };
            return Ok(existingUser);
        }
    }
}
