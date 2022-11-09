using RCL.Core.Azure.Storage;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AzureBlobStorageServiceCollectionExtension
    {
        public static IServiceCollection AddAzureBlobStorageServices(this IServiceCollection services,
            Action<AzureBlobStorageOptions> configureOptions)
        {
            services.AddTransient<IAzureBlobStorageService, AzureBlobStorageService>();
            services.Configure<AzureBlobStorageOptions>(configureOptions);
            return services;
        }
    }
}
