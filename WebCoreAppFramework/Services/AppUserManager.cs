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

namespace WebCoreAppFramework.Services
{
    public class AppUserManager : UserManager<ApplicationUser>, IAppUserManager
    {
        private readonly ApplicationDbContext DbContext;

        public AppUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
        IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
        IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger, ApplicationDbContext dbContext) :
        base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            DbContext = dbContext;
        }


        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, ApplicationTenant tenant, string role)
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

            IdentityResult rs = new IdentityResult();
            
            
            return rs;
        }
    }
}