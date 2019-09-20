using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WebCoreAppFramework.Models
{
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        [Required]
        public long TenantId { get; set; }
        [Required]
        public virtual ApplicationTenant Tenant { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}