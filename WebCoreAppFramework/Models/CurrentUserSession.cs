using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreAppFramework.Models
{
    public class CurrentUserSession
    {
        public CurrentUserSession(AppUserManager userManager, RoleManager<ApplicationRole> roleManager )
        {
           
        }
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationTenant Tenant { get; set; }


    }
}
