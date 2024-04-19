using dataa.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;


using service.Services.UserService.Dto;
using service.Services.TokenServices;




namespace service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _sigInManager;
        private readonly ITokenService _tokenService;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> sigInManager, ITokenService tokenService)

        {
            _userManager = userManager;
            _sigInManager = sigInManager;
            _tokenService = tokenService;
        }
    
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user == null)
            {
                return null;

            }
            var result = await _sigInManager.CheckPasswordSignInAsync(user, input.Password, false);
            if (!result.Succeeded)
            {

                throw new Exception("login faild ");
            }
            return new UserDto
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user)
            };
        
         }

        public async Task<UserDto> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user is not null)
                return null;

            var appUser = new AppUser
            {
                DisplayName=input.DisplayName,
                Email=input.Email,
                UserName=input.DisplayName
            };
            var result = await _userManager.CreateAsync(appUser, input.Password);
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.Select(x=>x.Description).FirstOrDefault());


            return new UserDto
            {
                Email = input.Email,
                DisplayName = input.DisplayName,
                Token = _tokenService.GenerateToken(appUser)
            };




        }
    }
}
