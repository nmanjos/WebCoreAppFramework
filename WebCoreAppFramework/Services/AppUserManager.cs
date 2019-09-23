using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebCoreAppFramework.Data;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.Options;
using WebCoreAppFramework.Services;

namespace Microsoft.AspNetCore.Identity
{
    public partial class AppUserManager : UserManager<ApplicationUser>, IAppUserManager
    {
        private readonly ApplicationDbContext DbContext;
        private readonly AppSetupOptions AppOptions;

        public AppUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger, ApplicationDbContext dbContext, AppSetupOptions options) :
        base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            DbContext = dbContext;
            AppOptions = options;
        }

        public async Task<ApplicationTenant> FindTenantByNameAsync(string TenantName)
        {
            var result = await Task.Run(() => DbContext.Tenants.SingleOrDefault(d => d.Name == TenantName));
            return result ;
          
        }
        


        public async Task<IdentityResult> CreateTenantAsync(string TenantName)
        {
            var user = DbContext.Users.SingleOrDefault(q => q.UserName == AppOptions.AdminUserName);
            if (await FindTenantByNameAsync(TenantName)==null)
            {
                await DbContext.Tenants.AddAsync(new ApplicationTenant { Name = TenantName, Manager = user });
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = "Tenant Already exists", Code= "UserExists" });
            }
            await DbContext.SaveChangesAsync();
            return IdentityResult.Success;
            
        }

        public async Task<IQueryable<ApplicationTenant>> GetTenantsAsync(ApplicationUser user)
        {
            var result = await Task.Run(() => DbContext.UserRoles.Where(q => q.User == user).Select(s => s.Tenant));
            return result;

        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, ApplicationTenant tenant, string role)
        {
            try
            {
                
                var Role = DbContext.Roles.Where(q => q.Name == role).FirstOrDefault();
                if (Role != null)
                {
                    ApplicationUserRole userRole = new ApplicationUserRole();
                    userRole.Role = Role;
                    userRole.RoleId = Role.Id;
                    userRole.Tenant = tenant;
                    userRole.TenantId = tenant.Id;
                    userRole.User = user;
                    userRole.UserId = user.Id;
                    var result = await DbContext.UserRoles.AddAsync(userRole);
                    await DbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

            return IdentityResult.Success;
        }
    }
}