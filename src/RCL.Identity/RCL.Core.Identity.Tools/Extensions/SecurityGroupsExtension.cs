using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using RCL.Core.Identity.Tools;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SecurityGroupsExtension
    {
        public static IServiceCollection AddRCLCoreIdentitySecurityGroupServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, GroupsCheckHandler>();
            services.AddTransient<IClaimsTransformation, GroupClaimsTransformation>();

            return services;
        }
    }
}
