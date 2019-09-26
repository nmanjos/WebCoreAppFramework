using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.ViewModels
{
    public class UserDetailsViewModel
    {
        [Key]
        public string Id { get; set; }
        public  ICollection<ApplicationUserClaim> Claims { get; set; }
        public  ICollection<ApplicationUserRole> UserRoles { get; set; }
        public  DateTimeOffset? LockoutEnd { get; set; }
        [PersonalData]
        public  bool TwoFactorEnabled { get; set; }
        [PersonalData]
        public  bool PhoneNumberConfirmed { get; set; }
         [PersonalData]
        public  bool EmailConfirmed { get; set; }
        [ProtectedPersonalData]
        public  string UserName { get; set; }
        public  bool LockoutEnabled { get; set; }
        public  int AccessFailedCount { get; set; }
    }
}
