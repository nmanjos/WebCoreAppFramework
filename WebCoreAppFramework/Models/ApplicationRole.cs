using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace WebCoreAppFramework.Models
{
    public class ApplicationRole : IdentityRole
    {
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    }
}