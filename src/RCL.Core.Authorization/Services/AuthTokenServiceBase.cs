using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace RCL.Core.Authorization
{ 
    internal abstract class AuthTokenServiceBase : IAuthTokenService
    {
        private readonly IAuthClientOptionsProvider _clientOptionsService;
        private readonly IAuthServerOptionsProvider _serviceOptionsService;
        private static readonly HttpClient client;

        static AuthTokenServiceBase()
        {
            client = new HttpClient();
        }

        public AuthTokenServiceBase(
            IAuthClientOptionsProvider clientOptionsService,
            IAuthServerOptionsProvider serviceOptionsService)
        {
            _clientOptionsService = clientOptionsService;
            _serviceOptionsService = serviceOptionsService;
        }

        public async Task<AuthToken> GetAuthTokenAsync(string resource)
        {
            AuthToken authToken = new AuthToken();

            try
            {
                client.DefaultRequestHeaders.Clear();

                AuthServerOptions _serviceOptions = await _serviceOptionsService.GetServiceOptionsAsync(resource);
                AuthClientOptions _clientOptions = await _clientOptionsService.GetClientOptionsAsync();

                var formcontent = new List<KeyValuePair<string, string>>();
                formcontent.Add(new KeyValuePair<string, string>(nameof(_serviceOptions.grant_type), _serviceOptions.grant_type));
                formcontent.Add(new KeyValuePair<string, string>(nameof(_clientOptions.client_id), _clientOptions.client_id));
                formcontent.Add(new KeyValuePair<string, string>(nameof(_clientOptions.client_secret), _clientOptions.client_secret));
                formcontent.Add(new KeyValuePair<string, string>(nameof(_serviceOptions.resource), _serviceOptions.resource));

                var request = new HttpRequestMessage(HttpMethod.Post, _serviceOptions.endpoint) { Content = new FormUrlEncodedContent(formcontent) };

                var response = await client.SendAsync(request);
                string jstr = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    authToken = JsonSerializer.Deserialize<AuthToken>(jstr);
                }
                else
                {
                    throw new Exception($"Could not obtain auth token. {jstr}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return authToken;
        }
    }
}
