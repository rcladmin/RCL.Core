using Microsoft.Azure.Functions.Worker.Http;

namespace RCL.Core.Api.Authorization
{
    internal class ClientCredentialsAuthorization : IApiAuthorization
    {
        public bool IsAuthorized(HttpRequestData req, string ClientIds = "", string SecretKeyName = "", 
            string SecretKeyValue = "", bool isApiPrivate = false)
        {
            bool b = false;

            string accesstoken = AuthTokenHelper.GetAccessToken(req);

            if (!string.IsNullOrEmpty(accesstoken))
            {
                string clientId = AuthTokenHelper.GetClientId(accesstoken);

                if (!string.IsNullOrEmpty(clientId))
                {
                    List<string> clientIds = ClientIds.Split(',').ToList();

                    if (clientIds?.Count > 0)
                    {
                        if (isApiPrivate == true)
                        {
                            if (clientId == clientIds.First())
                            {
                                b = true;
                            }
                        }
                        else
                        {
                            if (clientIds.Contains(clientId))
                            {
                                b = true;
                            }
                        }
                    }
                }
            }

            return b;
        }
    }
}
