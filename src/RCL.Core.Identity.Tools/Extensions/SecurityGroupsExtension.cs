using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace RCL.Core.Identity.Tools
{
    public static class SecurityGroupsExtension
    {
        public static IServiceCollection AddRCLIdentitySecurityGroupServices(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, GroupsCheckHandler>();
            services.AddTransient<IClaimsTransformation, GroupClaimsTransformation>();

            return services;
        }
    }
}
