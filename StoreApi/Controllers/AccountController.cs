using dataa.Entities.IdentityEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using service.HandlerResponse;
using service.Services.UserService;
using service.Services.UserService.Dto;
using System.Security.Claims;

namespace StoreApi.Controllers
{
  
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController (IUserService userService, UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
        {
            
            var user = await _userService.Login(input);
            if (user == null)
            {
                return Unauthorized(new customException(401));
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto input)
        {

            var user = await _userService.Register(input);
            if (user == null)
            {
                return BadRequest(new customException(400,"Email Already Exist"));
            }
            return Ok(user);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUserDetails()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
               
            };

        }
    }
}
