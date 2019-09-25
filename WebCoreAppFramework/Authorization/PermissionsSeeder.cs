using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                foreach (var subPermission in typeof(Permissions).GetNestedTypes())
                {
                    foreach (var field in subPermission.GetFields())
                    {
                        string policy = field.GetValue(null).ToString();
                        perm.AddPolicy(policy, builder =>
                        {
                            builder.AddRequirements(new PermissionRequirement(policy));
                        });
                    }
                }
            });
        }
    }
}
