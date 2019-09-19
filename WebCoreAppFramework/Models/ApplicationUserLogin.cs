using Microsoft.AspNetCore.Identity;

namespace WebCoreAppFramework.Models
{
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}