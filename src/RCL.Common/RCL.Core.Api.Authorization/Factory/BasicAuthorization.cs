using Microsoft.Azure.Functions.Worker.Http;
using System.Text;

namespace RCL.Core.Api.Authorization
{
    internal class BasicAuthorization : IApiAuthorization
    {
        public bool IsAuthorized(HttpRequestData req, string ClientIds = "", 
            string SecretKeyName = "", string SecretKeyValue = "", bool isApiPrivate = false)
        {
            bool b = false;

            if (req.Headers.TryGetValues("Authorization", out var values))
            {
                string authHeader = values?.FirstOrDefault() ?? string.Empty;

                if (authHeader != string.Empty && authHeader.StartsWith("Basic"))
                {
                    string encodedUsernamePassword = authHeader.Substring("Basic ".Length).Trim();
                    Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                    string usernamePassword = encoding.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    int seperatorIndex = usernamePassword.IndexOf(':');

                    var username = usernamePassword.Substring(0, seperatorIndex);
                    var password = usernamePassword.Substring(seperatorIndex + 1);

                    if (username == SecretKeyName && password == SecretKeyValue)
                    {
                        b = true;
                    }
                }
            }

            return b;
        }
    }
}
