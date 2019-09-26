using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.ViewModels
{
    public class UserIndexViewModel
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<UserRolesViewModel> UserRoles { get; set; }

    }
}
