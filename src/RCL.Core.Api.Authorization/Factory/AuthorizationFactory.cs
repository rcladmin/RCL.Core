#nullable disable

namespace RCL.Core.Api.Authorization
{
    internal class AuthorizationFactory : IAuthorizationFactory
    {
        public IApiAuthorization Create(AuthType authType)
        {
            if(authType == AuthType.ClientCredentials)
            {
                return new ClientCredentialsAuthorization();
            }
            else if(authType == AuthType.SecretKey)
            {
                return new SecurityKeyAuthorization();
            }
            else if (authType == AuthType.BasicAuth)
            {
                return new BasicAuthorization();
            }
            else
            {
                return null;
            }
        }
    }
}
