using Microsoft.Extensions.Options;
using RCL.Core.Api.RequestService;
using RCL.Core.Authorization;

namespace RCL.Core.Identity.Proofing
{
    internal class IdentityApproverService : ApiRequestBase, IIdentityApproverService
    {
        private readonly IOptions<IdentityProofingApiOptions> _options;

        public IdentityApproverService(IAuthTokenService authTokenService,
            IOptions<IdentityProofingApiOptions> options) 
            : base(authTokenService)
        {
            _options = options;
        }

        public async Task<IdentityApprover> GetIdentityApproverByIdentifierAsync(string subscrid, string identifier)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/identityapprover/subscriptionid/{subscrid}/identifier/{identifier}/get";

            IdentityApprover _organization = await GetAsync<IdentityApprover>(uri, _options.Value.Resource);

            return _organization;
        }
    }
}
