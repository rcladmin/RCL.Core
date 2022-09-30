using Microsoft.Extensions.Options;

namespace RCL.Core.Authorization
{
    internal class AuthTokenService : AuthTokenServiceBase, IAuthTokenService
    {
        public AuthTokenService(IAuthClientOptionsProvider clientOptionsProvider,
            IAuthServerOptionsProvider serverOptionsProvider)
            : base(clientOptionsProvider,serverOptionsProvider)
        {
        }
    }
}
