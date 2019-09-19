using Microsoft.AspNetCore.Identity;

namespace WebCoreAppFramework.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole Role { get; set; }
    }
}