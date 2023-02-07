#nullable disable

using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using System.Net.Http.Headers;

namespace RCL.Core.Identity.Graph
{
    public class GraphService : IGraphService
    {
        private GraphServiceClient _graphClient = null;
        private readonly IOptions<AuthOptions> _authOptions;

        public GraphService(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions;
        }

        private void GetGraphServiceClient()
        {
            DelegateAuthenticationProvider del = new DelegateAuthenticationProvider(AuthenticationProvider);
            _graphClient = new GraphServiceClient(del);
        }

        public async Task<User> CreateUser(User user)
        {
            try
            {
                GetGraphServiceClient();
                User createdUser = await _graphClient.Users
                .Request()
                .AddAsync(user);

                OpenTypeExtension extension = new OpenTypeExtension
                {
                    ExtensionName = "DigitalIdentity-Custom-Extensions",
                    AdditionalData = user.AdditionalData
                };

                await _graphClient.Users[createdUser.Id].Extensions.Request()
                    .AddAsync(extension);

                return createdUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByPrincipalNameAsync(string principalName)
        {
            try
            {
                GetGraphServiceClient();
                User user = await _graphClient.Users[principalName]
                    .Request()
                    .Select(e => new
                    {
                        e.GivenName,
                        e.Surname,
                        e.StreetAddress,
                        e.City,
                        e.State,
                        e.PostalCode,
                        e.Country,
                        e.UserPrincipalName,
                        e.DisplayName,
                        e.Id,
                        e.AdditionalData
                    })
                    .GetAsync();

                var extensions = await _graphClient.Users[user.Id].Extensions.Request().GetAsync();
                user.Extensions = extensions;

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserByObjectIdAsync(string objectId)
        {
            try
            {
                GetGraphServiceClient();
                User user = await _graphClient.Users[objectId]
                    .Request()
                    .Select(e => new
                    {
                        e.GivenName,
                        e.Surname,
                        e.StreetAddress,
                        e.City,
                        e.State,
                        e.PostalCode,
                        e.Country,
                        e.UserPrincipalName,
                        e.DisplayName,
                        e.Id,
                        e.AdditionalData
                    })
                    .GetAsync();

                var extensions = await _graphClient.Users[user.Id].Extensions.Request().GetAsync();
                user.Extensions = extensions;

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<User>> GetUsersByNameAsync(string givenName, string surname)
        {
            try
            {
                GetGraphServiceClient();

                List<User> users = new List<User>();

                var page = await _graphClient.Users
                    .Request()
                    .Filter($"GivenName eq '{givenName}' and Surname eq '{surname}'")
                    .Select(e => new
                    {
                        e.GivenName,
                        e.Surname,
                        e.StreetAddress,
                        e.City,
                        e.State,
                        e.PostalCode,
                        e.Country,
                        e.UserPrincipalName,
                        e.DisplayName,
                        e.Id,
                        e.AdditionalData
                    })
                    .GetAsync();

                if(page.ToList().Count > 0)
                {
                    foreach(var user in page.ToList())
                    {
                        var extensions = await _graphClient.Users[user.Id].Extensions.Request().GetAsync();
                        user.Extensions = extensions;

                        users.Add(user);
                    }
                }

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<string>> GetUserInGroupsByObjectIdAsync(string objectId)
        {
            try
            {
                GetGraphServiceClient();
                var page = await _graphClient.Users[objectId]
                    .MemberOf
                    .Request()
                    .GetAsync();

                var groupNames = new List<string>();
                groupNames.AddRange(page
                        .OfType<Group>()
                        .Select(x => x.DisplayName)
                        .Where(name => !string.IsNullOrEmpty(name)));
                while (page.NextPageRequest != null)
                {
                    page = await page.NextPageRequest.GetAsync();
                    groupNames.AddRange(page
                        .OfType<Group>()
                        .Select(x => x.DisplayName)
                        .Where(name => !string.IsNullOrEmpty(name)));
                }

                return groupNames;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Dictionary<string,object> GetUserCustomAttributes(User user)
        {
            Dictionary<string, object> customAttributes = new Dictionary<string, object>();

            try
            {
                if(user?.Extensions?.CurrentPage?.Count > 0)
                {
                    var extension = user.Extensions.CurrentPage.FirstOrDefault();
                  
                    if(extension?.AdditionalData?.Count > 0)
                    {
                        customAttributes = (Dictionary<string, object>)extension.AdditionalData;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customAttributes;
        }

        public async Task DeleteUserByObjectIdAsync(string objectId)
        {
            try
            {
                GetGraphServiceClient();
                await _graphClient.Users[objectId]
                .Request()
                .DeleteAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task AuthenticationProvider(HttpRequestMessage requestMessage)
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
            .Create(_authOptions.Value.ClientId)
            .WithTenantId(_authOptions.Value.TenantId)
            .WithClientSecret(_authOptions.Value.ClientSecret)
            .Build();

            var scopes = new string[] { "https://graph.microsoft.com/.default" };

            var authResult = await confidentialClientApplication
               .AcquireTokenForClient(scopes)
               .ExecuteAsync();

            requestMessage.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
        }
    }
}
