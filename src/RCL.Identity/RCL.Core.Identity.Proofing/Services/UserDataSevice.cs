using Microsoft.Extensions.Options;
using RCL.Core.Api.RequestService;
using RCL.Core.Authorization;

namespace RCL.Core.Identity.Proofing
{
    internal class UserDataSevice : ApiRequestBase, IUserDataService
    {
        private readonly IOptions<IdentityProofingApiOptions> _options;

        public UserDataSevice(IAuthTokenService authTokenService,
            IOptions<IdentityProofingApiOptions> options) : base(authTokenService)
        {
            _options = options;
        }

        public async Task ApproveUserDataAsync(string subscrid, UserData userData)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/userdata/subscriptionid/{subscrid}/approve";

            await PostAsync<UserData>(uri,_options.Value.Resource, userData);
        }

        public async Task<UserData> GetUserDataByIdAsync(string subscrid, int id)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/userdata/subscriptionid/{subscrid}/id/{id}/get";

            UserData _userData = await GetAsync<UserData>(uri, _options.Value.Resource);

            return _userData;
        }

        public async Task<List<UserData>> GetUserDataByIdentityApproverAsync(string subscrid, string identityApproverIdentifier)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/userdata/subscriptionid/{subscrid}/identityapproveridentifier/{identityApproverIdentifier}/get";

            List<UserData> _userDatas = await GetListResultAsync<UserData>(uri, _options.Value.Resource);

            return _userDatas;
        }

        public async Task DeleteUserDataAsync(string subscrid, int id)
        {
            string uri = $"{_options.Value.ApiEndpoint}/v1/did/userdata/subscriptionid/{subscrid}/id/{id}/delete";

            await DeleteAsync(uri, _options.Value.Resource);

        }
    }
}
