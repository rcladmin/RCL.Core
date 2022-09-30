using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace RCL.Core.Authorization
{
    internal class ServerOptionsProvider : IAuthServerOptionsProvider
    {
        private readonly IOptions<AuthOptions> _options;

        public ServerOptionsProvider(IOptions<AuthOptions> options)
        {
            _options = options;
        }

        public Task<AuthServerOptions> GetServiceOptionsAsync(string resource)
        {
            return Task.Run(() => new AuthServerOptions
            {
                endpoint = $"https://login.microsoftonline.com/{_options.Value.TenantId}/oauth2/token",
                grant_type = "client_credentials",
                resource = resource
            });
        }
    }
}
