using dataa.context;
using dataa.Entities.IdentityEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace StoreApi.Extensions
{
    public static class IdentityServicesEx
    {
      public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration _configuration) 
        { 
            var builder= services.AddIdentityCore<AppUser>();
            builder=new IdentityBuilder(builder.UserType,builder.Services);
            builder.AddEntityFrameworkStores<StoreIdentityDbContext>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"])),
                        ValidateIssuer= true,
                        ValidIssuer = _configuration["Token:Issuer"],
                        ValidateAudience=false






                    };

                });

            return services;

        }


    }
}
