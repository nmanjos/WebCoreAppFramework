using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Authorization;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.ViewModels
{
    public class RoleClaimsAddViewModel
    {
        public string RoleId { get; set; }
            


        public List<RoleClaim> List { get; set; }
       
    }
}
