#nullable disable

using Microsoft.Azure.Functions.Worker.Http;
using System.IdentityModel.Tokens.Jwt;

namespace RCL.Core.Api.Authorization
{
    internal static class AuthTokenHelper
    {
        internal static string GetAccessToken(HttpRequestData req)
        {
            string authToken = string.Empty;

            try
            {
                authToken = req.Headers
                    .Where(w => w.Key == "Authorization").FirstOrDefault()
                    .Value.FirstOrDefault()
                    .Replace("Bearer", "")?
                    .TrimStart();
            }
            catch (Exception) { }

            return authToken;
        }

        internal static string GetClientId(string accessToken)
        {
            string clientId = string.Empty;

            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken token = handler.ReadJwtToken(accessToken);

                var claims = token.Claims;

                clientId = claims?.FirstOrDefault(c => c.Type == "appid")?.Value ?? string.Empty;

                if (clientId == string.Empty)
                {
                    clientId = claims?.FirstOrDefault(c => c.Type == "azp")?.Value ?? string.Empty;
                }
            }
            catch (Exception)
            {
                return clientId;
            }

            return clientId;
        }
    }
}
