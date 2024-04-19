using dataa.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos
{
    public class AppIdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userMaanager)
        {
            if (!userMaanager.Users.Any()) 
            {
                var user = new AppUser
                {
                    DisplayName = "Mostafa",
                    Email = "Mostafa@gmail.com",
                    UserName = "mostafaeid",
                    Address = new Address
                    {
                        FirstName = "mostafa",
                        LastName = "eid",
                        State = "banha",
                        City="banha",
                        Street = "22",
                        ZipCode = "12345"

                    }
                };
                await userMaanager.CreateAsync(user,"Password123!");

            }
        
        }

    }
}
