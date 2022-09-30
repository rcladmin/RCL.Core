using RCL.Core.Identity.Graph;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class GraphExtension
    {
        public static IServiceCollection AddRCLCoreIdentityGraphServices(this IServiceCollection services)
        {
            services.AddTransient<IGraphService, GraphService>();
            return services;
        }
    }
}
