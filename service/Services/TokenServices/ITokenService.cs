using dataa.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.TokenServices
{
    public interface ITokenService
    {
         string GenerateToken(AppUser appUser);

    }
}
