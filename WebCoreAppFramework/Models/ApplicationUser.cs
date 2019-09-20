using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreAppFramework.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationTenant> Tenants { get; set; }
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public Address Address { get; set; }
        public string FiscalNumber  { get; set; }
        public Language Language { get; set; }


    }
}
