using service.Services.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.UserService
{
    public interface IUserService
    {
        
        Task<UserDto> Register(RegisterDto input);
        Task<UserDto> Login(LoginDto input);

    }
}
