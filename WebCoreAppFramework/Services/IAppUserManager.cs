using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Models;

namespace WebCoreAppFramework.Services
{
    public interface IAppUserManager 
    {
        string GetDefaultTenantName();
        Task<bool> SetCurrentUserLocation(string UserName, double latitude, double longitude);

        Task<IdentityResult> AddToRoleAsync(ApplicationUser user, ApplicationTenant tenant, string role);

        Task<IQueryable<ApplicationTenant>> GetTenantsAsync(string UserEmail);

        Task<IQueryable<ApplicationTenant>> GetTenantsAsync(ApplicationUser user);

        ApplicationTenant FindTenantByName(string TenantName);

        Task<IdentityResult> CreateTenantAsync(string TenantName, ApplicationUser Manager);

        Task<IdentityResult> SetCurrentSession(string email, string tenantName);
        
    }
}