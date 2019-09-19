using Microsoft.AspNetCore.Identity;

namespace WebCoreAppFramework.Models
{
    public class ApplicationUserClaim : IdentityUserClaim<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}