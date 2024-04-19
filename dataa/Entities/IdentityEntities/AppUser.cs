using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace dataa.Entities.IdentityEntities
{
    public class AppUser: IdentityUser
    {
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
