using Microsoft.AspNetCore.Identity;

namespace WebCoreAppFramework.Models
{
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser User { get; set; }
    }
}