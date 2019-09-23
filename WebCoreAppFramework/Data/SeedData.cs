﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Models;
using WebCoreAppFramework.Options;
using WebCoreAppFramework.Services;

namespace WebCoreAppFramework.Data
{
    public static class SeedData
    {

        public static async Task Initialize(ApplicationDbContext context,
                              AppUserManager userManager,
                              RoleManager<ApplicationRole> roleManager, AppSetupOptions options, ILogger logger)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (userManager is null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            if (roleManager is null)
            {
                throw new ArgumentNullException(nameof(roleManager));
            }

            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (logger is null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            context.Database.EnsureCreated();

            



            try
            {
                if (await roleManager.FindByNameAsync(options.AdminRoleName) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = options.AdminRoleName });
                }

                if (await roleManager.FindByNameAsync(options.ManagerRoleName) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = options.ManagerRoleName });
                }

                if (await roleManager.FindByNameAsync(options.NormalRoleName) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = options.NormalRoleName });
                }

                ApplicationTenant tenant = await userManager.FindTenantByNameAsync(options.DefaultTenantName);

                if (tenant == null)
                {
                    tenant.Name = options.DefaultTenantName;

                }

                ApplicationUser user = await userManager.FindByNameAsync(options.AdminUserName);
                
                if (user == null)
                {
                    user.UserName = options.AdminUserName;
                    user.Email = options.AdminUserName;

                    var result = await userManager.CreateAsync(user, options.AdminUserPass);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, tenant, options.AdminRoleName);
                    }
                }

               




            }
            catch (Exception ex)
            {

                logger.LogError(ex, "An error occurred while seeding user.");
                //throw;
            }



        }
    }
}
