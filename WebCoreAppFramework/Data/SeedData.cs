using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebCoreAppFramework.Authorization;
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
            logger.LogInformation("Data Seeder Started");
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
                    var adminRole = await roleManager.FindByNameAsync(options.AdminRoleName);
                    await roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, Permissions.AdminUser.Read));
                }

                if (await roleManager.FindByNameAsync(options.ManagerRoleName) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = options.ManagerRoleName });
                }

                if (await roleManager.FindByNameAsync(options.NormalRoleName) == null)
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = options.NormalRoleName });
                }



                ApplicationUser user = await userManager.FindByNameAsync(options.AdminUserName);

                if (user == null)
                {
                    user = new ApplicationUser();
                    user.UserName = options.AdminUserName;
                    user.EmailConfirmed = true;
                    user.Email = options.AdminUserName;
                    var result = await userManager.CreateAsync(user, options.AdminUserPass);
                    var tenantresult = await userManager.CreateTenantAsync(options.DefaultTenantName, user);

                    if (result.Succeeded && tenantresult.Succeeded)
                    {
                        ApplicationTenant tenant = userManager.FindTenantByName(options.DefaultTenantName);
                        await userManager.AddToRoleAsync(user, tenant, options.AdminRoleName);
                    }
                }
                List<GeoCity> listOfLocations = new List<GeoCity>();
                IOrderedEnumerable<Country> countries = null;
                if (!context.Countries.Any())
                {
                    listOfLocations.AddRange(File.ReadLines("Data\\worldcities.csv").Select(line => new GeoCity(line)).ToList());
                    countries = listOfLocations.Select(s => new Country { Name = s.Country, CountryCode = s.Iso2 }).GroupBy(g => g.Name).Select(q => q.First()).OrderBy(o => o.Name);

                    var i = countries.Count();

                    await context.Countries.AddRangeAsync(countries);
                    await context.SaveChangesAsync();
                }

                if (!context.GeoLocations.Any() || countries == null)
                {
                    foreach (var location in listOfLocations)
                    {
                        GeoLocation Location = new GeoLocation();
                        Location.isCity = true;
                        Location.Name = location.City;
                        Location.Latitude = location.Lat;
                        Location.Longitude = location.Lng;
                        Location.Country = countries.FirstOrDefault(q => q.CountryCode == location.Iso2);
                        await context.GeoLocations.AddAsync(Location);
                    }
                }
               

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
                //throw;
            }



        }
    }
}
