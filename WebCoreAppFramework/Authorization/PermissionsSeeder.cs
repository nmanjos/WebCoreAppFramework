using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCoreAppFramework.Authorization;
using WebCoreAppFramework.Services;

namespace WebCoreAppFramework.Authorization
{
    public static class PermissionsSeeder
    {
        public static void Initialize(IServiceCollection services)
        {
            services.AddAuthorization(perm =>
            {
                var permissions = typeof(Permissions);

                var sub_validator_types =
                    permissions
                    .Assembly
                    .DefinedTypes
                    .Where(x => x.IsAssignableFrom(permissions))
                    
                    .ToList();


                //var sub_validator_types = permissions.GetFields();


                foreach (var permission in sub_validator_types[0].DeclaredMembers)
                {
                    
                    perm.AddPolicy(permission.Name, builder =>
                    {
                        builder.AddRequirements(new PermissionRequirement(permission.Name));
                    });
                }

           
            });
        }
    }
}
