using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Authorization;

namespace WebCoreAppFramework.ViewModels
{
    public class RoleClaimViewModel
    {
        
        public string RoleId { get; set; }
        public string RoleName { get; set; }

        public string Claim { get; set; }
        public bool Active { get; set; }

        
        

    }
}
