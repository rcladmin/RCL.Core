using RCL.Core.Authorization;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthorizationExtension
    {
        public static IServiceCollection AddRCLCoreAuthTokenServices(this IServiceCollection services, Action<AuthOptions> setupAction)
        {
            services.AddTransient<IAuthClientOptionsProvider, ClientOptionsProvider>();
            services.AddTransient<IAuthServerOptionsProvider, ServerOptionsProvider>();
            services.AddTransient<IAuthTokenService, AuthTokenService>();
            services.Configure(setupAction);

            return services;
        }
    }
}
