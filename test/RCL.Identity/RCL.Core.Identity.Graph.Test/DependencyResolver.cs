using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RCL.Core.Identity.Graph.Test
{
    public static class DependencyResolver
    {
        public static ServiceProvider ServiceProvider()
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddUserSecrets<TestProject>();
            IConfiguration Configuration = builder.Build();

            IServiceCollection services = new ServiceCollection();
            services.AddRCLCoreAuthTokenServices(options => Configuration.Bind("Auth", options));
            services.AddRCLCoreIdentityGraphServices();

            return services.BuildServiceProvider();
        }
    }

    public class TestProject
    {
    }
}
