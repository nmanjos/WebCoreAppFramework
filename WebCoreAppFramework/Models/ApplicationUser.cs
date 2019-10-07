using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreAppFramework.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
        public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
        public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }

        public string Name { get; set; }
        public CurrentUserSession CurrentSession {get; set;}
        public Address Address { get; set; }
        public string FiscalNumber  { get; set; }
        public Language Language { get; set; }
        public Country Country { get; set; }

        public bool Visible { get; set; } = true; // to maitain integrity Delete actions do not Delete, they set this field to false 
        public bool System { get; set; } = false; // this field Locks update and deletes   Sistem Records can only be changed in the database.
    }
}
