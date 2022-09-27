using Microsoft.Azure.Functions.Worker.Http;

namespace RCL.Core.Api.Authorization
{
    internal class SecurityKeyAuthorization : IApiAuthorization
    {

        public bool IsAuthorized(HttpRequestData req, string ClientIds = "", string SecretKeyName = "", string SecretKeyValue = "", bool isApiPrivate = false)
        {
            bool b = false;

            var headers = req.Headers;

            string _securityKeyValue = string.Empty;

            if (headers.TryGetValues(SecretKeyName, out var keys))
            {
                _securityKeyValue = keys?.FirstOrDefault() ?? string.Empty;
            }

            if (_securityKeyValue == SecretKeyValue)
            {
                b = true;
            }

            return b;
        }
    }
}
