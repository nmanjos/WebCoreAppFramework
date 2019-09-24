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


namespace WebCoreAppFramework.Services
{
    public partial class AppUserManager : UserManager<ApplicationUser>, IAppUserManager
    {
        private readonly ApplicationDbContext DbContext;
        private readonly AppSetupOptions AppOptions;

        private readonly ILogger logger;

        public AppUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<AppUserManager> Logger,
        ApplicationDbContext dbContext, IOptions<AppSetupOptions> options) :
        base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, Logger)
        {
            logger = Logger;
            DbContext = dbContext;
            AppOptions = options.Value;
            logger.LogInformation("AppUserManager Initialized");
        }

        public ApplicationTenant FindTenantByName(string TenantName)
        {
            try
            {
                logger.LogInformation($"Query Tennant: {TenantName}");
                ApplicationTenant result = DbContext.Tenants.SingleOrDefault(d => d.Name == TenantName);
                logger.LogInformation($"result is null?: {result == null}");
                return result;
            }
            catch (System.Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }

        }
        public async Task<IdentityResult> CreateTenantAsync(string TenantName, ApplicationUser Manager)
        {
            
            if (FindTenantByName(TenantName) == null)
            {
                await DbContext.Tenants.AddAsync(new ApplicationTenant { Name = TenantName, Manager = Manager });
            }
            else
            {
                return IdentityResult.Failed(new IdentityError { Description = "Tenant Already exists", Code = "UserExists" });
            }
            await DbContext.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public async Task<IQueryable<ApplicationTenant>> GetTenantsAsync(string UserEmail)
        {
            return await this.GetTenantsAsync(await base.FindByEmailAsync(UserEmail));
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
                    return IdentityResult.Success;
                }
                // if we got this far is because we did not found the role.
                return IdentityResult.Failed(new IdentityError { Code = "RoleNotFound", Description = "Role not found" });


            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Code = ex.HResult.ToString(), Description = ex.Message });

            }
        }

        public async Task<IdentityResult> SetCurrentSession(string email, string tenantName)
        {
            var user = await base.FindByEmailAsync(email);
            var tenant = FindTenantByName(tenantName);
            if (tenant != null)
            {
                user.CurrentSession.Tenant = tenant;
                return IdentityResult.Success;
            }

            return IdentityResult.Failed(new IdentityError { Code = "TenantNotFound", Description = "Tenant not found" });
        }
    }
}
