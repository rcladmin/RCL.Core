using Microsoft.Extensions.DependencyInjection;

namespace RCL.Core.Identity.Proofing
{
    public static class IdentityProofingExtension
    {
        public static IServiceCollection AddRCLCoreIdentityProofingServices(this IServiceCollection services,
            Action<IdentityProofingApiOptions> setupApiAction)
        {
            services.AddTransient<IUserDataService, UserDataSevice>();
            services.AddTransient<IOrganizationDataService, OrganizationDataService>();

            services.Configure(setupApiAction);
            
            return services;
        }
    }
}
