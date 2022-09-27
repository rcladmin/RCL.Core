using Microsoft.Azure.Functions.Worker.Http;

namespace RCL.Core.Api.Authorization
{
    public interface IApiAuthorization
    {
        bool IsAuthorized(HttpRequestData req, string ClientIds = "",
            string SecretKeyName = "", string SecretKeyValue = "", bool isApiPrivate = false);
    }
}
