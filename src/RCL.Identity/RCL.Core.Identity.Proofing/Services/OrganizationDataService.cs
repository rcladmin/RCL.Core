using Microsoft.Extensions.Options;
using RCL.Core.Api.RequestService;
using RCL.Core.Authorization;

namespace RCL.Core.Identity.Proofing
{
    internal class OrganizationDataService : ApiRequestBase, IOrganizationDataService
    {
        private readonly IOptions<IdentityProofingApiOptions> _options;

        public OrganizationDataService(IAuthTokenService authTokenService,
            IOptions<IdentityProofingApiOptions> options) 
            : base(authTokenService)
        {
            _options = options;
        }

        public async Task<OrganizationData> CreateOrganizationAsync(string subscrid, OrganizationData organization)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/organizationdata/subscriptionid/{subscrid}/create";

            OrganizationData _organization = await PostAsync<OrganizationData, OrganizationData>(uri, _options.Value.Resource, organization);

            return _organization;
        }

        public async Task<OrganizationData> UpdateOrganizationDataAsync(string subscrid, OrganizationData organization)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/organizationdata/subscriptionid/{subscrid}/id/{organization.Id}/update";

            OrganizationData _organization = await PutAsync<OrganizationData, OrganizationData>(uri, _options.Value.Resource, organization);

            return _organization;
        }

        public async Task<OrganizationData> GetOrganizationDataByIdAsync(string subscrid, int id)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/organizationdata/subscriptionid/{subscrid}/id/{id}/get";

            OrganizationData _organization = await GetAsync<OrganizationData>(uri, _options.Value.Resource);

            return _organization;
        }

        public async Task<OrganizationData> GetOrganizationDataBySubscriptionAsync(string subscrid, int subscribid)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/organizationdata/subscriptionid/{subscrid}/subscribid/{subscribid}/get";

            OrganizationData _organization = await GetAsync<OrganizationData>(uri, _options.Value.Resource);

            return _organization;
        }

        public async Task DeleteOrganizationDataAsync(string subscrid, int id)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/organizationdata/subscriptionid/{subscrid}/id/{id}/delete";

            await DeleteAsync(uri, _options.Value.Resource);
        }
    }
}
