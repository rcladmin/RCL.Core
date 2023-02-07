using Microsoft.Extensions.DependencyInjection;

namespace RCL.Core.Identity.Graph
{
    public static class GraphExtension
    {
        public static IServiceCollection AddRCLIdentityGraphServices(this IServiceCollection services, 
            Action<AuthOptions> setupAction)
        {
            services.AddTransient<IGraphService, GraphService>();
            services.Configure(setupAction);
            return services;
        }
    }
}
