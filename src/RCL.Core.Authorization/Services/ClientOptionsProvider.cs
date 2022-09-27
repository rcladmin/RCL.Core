using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace RCL.Core.Authorization
{
    internal class ClientOptionsProvider : IAuthClientOptionsProvider
    {
        private readonly IOptions<AuthOptions> _options;

        public ClientOptionsProvider(IOptions<AuthOptions> options)
        {
            _options = options;
        }

        public Task<AuthClientOptions> GetClientOptionsAsync()
        {
            return Task.Run(() => new AuthClientOptions
            {
                client_id = _options.Value.ClientId,
                client_secret = _options.Value.ClientSecret,
                tenantId = _options.Value.TenantId
            });
        }
    }
}
