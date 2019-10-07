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


        public string GetDefaultTenantName()
        {
            return AppOptions.DefaultTenantName;
        }

        public async Task<bool> SetCurrentUserLocation(string UserName, double latitude, double longitude)
        {
            bool result = false;
            try
            {
                
                var user = await base.FindByNameAsync(UserName);
                if (user.CurrentSession == null) user.CurrentSession = new CurrentUserSession();

                if (user.CurrentSession.GeoLocation == null) user.CurrentSession.GeoLocation = new GeoLocation();
                user.CurrentSession.GeoLocation.Latitude = latitude;
                user.CurrentSession.GeoLocation.Longitude = longitude;
                await this.UpdateAsync(user);
            }
            catch (Exception ex)
            {

                Logger.LogError(ex.Message);
            }
            

            return result;
        }

        /// <summary>
        /// Find Tenant by name
        /// </summary>
        /// <param name="TenantName"></param>
        /// <returns></returns>
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

        public IQueryable<ApplicationUser>  FindByTenantId(long? Id)
        {
            if (Id != null)
            {
                return (DbContext.UserRoles.Where(q => q.TenantId == Id)).Select(s => s.User);
            }
            return null;
        }

        public async Task<IdentityResult> UpdateTenantAsync(ApplicationTenant Tenant)
        {
            try
            {
                DbContext.Tenants.Update(Tenant);
                await DbContext.SaveChangesAsync();
                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return IdentityResult.Failed(new IdentityError { Description = $"Unable to Update Tenant: {ex.Message}", Code = "UserUnUpdatable" }); ;
            }
        }

        /// <summary>
        /// Create Tenant, Manager user must exist so it can be added to Tenant
        /// </summary>
        /// <param name="TenantName" type="string"></param>
        /// <param name="Manager" type="ApplicationUser"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Sets Current User session 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tenantName"></param>
        /// <returns></returns>
        public async Task<ApplicationUser> SetCurrentSession(string email, string tenantName)
        {

            var user = DbContext.Users.Find((await base.FindByEmailAsync(email)).Id);
            var tenant = FindTenantByName(tenantName);
            if (tenant != null)
            {
                CurrentUserSession currentUserSession = user.CurrentSession;
                if (currentUserSession == null)
                {
                    currentUserSession = new CurrentUserSession();
                }
                currentUserSession.Tenant = tenant;
                user.CurrentSession = currentUserSession;
                await base.UpdateAsync(user);
                await DbContext.Entry(user).Collection(p => p.UserRoles).LoadAsync();
                await DbContext.Entry(user).Collection(p => p.Claims).LoadAsync();
                this.UpdateAsync(user);
                return user;
            }

            return null;
        }

        Task<IdentityResult> IAppUserManager.SetCurrentSession(string email, string tenantName)
        {
            throw new NotImplementedException();
        }
    }
}
