﻿using dataa.Entities.IdentityEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.TokenServices
{
    public class TokenService:ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration) 
        {
            _configuration = configuration;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
        }

        public string GenerateToken(AppUser appUser)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim(ClaimTypes.GivenName, appUser.DisplayName),


            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Token:Issuer"],
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds,




            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);



        }
    }
}
