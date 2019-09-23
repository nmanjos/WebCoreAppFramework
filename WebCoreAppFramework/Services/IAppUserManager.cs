using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.Services
{
    public interface IAppUserManager 
    {
        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, ApplicationTenant tenant, string role);
    }
}