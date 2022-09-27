#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RCL.Core.Identity.Graph;

namespace RCL.Core.Identity.Proofing.UI.Pages.UserApproval
{
    [Authorize(Policy = "UserAdmins")]
    public class ApproveModel : PageModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IGraphService _graphService;
        private readonly IOptions<IdentityProofingApiOptions> _options;

        public UserData UserData { get; set; } = new UserData();
        public string ErrorMessage { get; set; } = string.Empty;

        public ApproveModel(IUserDataService userDataService,
            IGraphService graphService,
            IOptions<IdentityProofingApiOptions> options)
        {
            _userDataService = userDataService;
            _graphService = graphService;
            _options = options;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                await GetUserDataAsync(id);

                if (string.IsNullOrEmpty(UserData?.Email))
                {
                    ErrorMessage = "The user was not found";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await GetUserDataAsync(id);

                if (string.IsNullOrEmpty(UserData?.Email))
                {
                    ErrorMessage = "The user was not found";
                }

                Microsoft.Graph.User graphUser = UserConverter.ConvertToGraphUser(UserData);

                Microsoft.Graph.User newUser = await CreateGraphUserAsync(graphUser);

                if(!string.IsNullOrEmpty(newUser?.Id))
                {
                    UserData.ObjectId = newUser.Id;
                    UserData.UserPrincipalName = newUser.UserPrincipalName;
                    UserData.TemporaryPassword = graphUser.PasswordProfile.Password;

                    await _userDataService.ApproveUserDataAsync(_options.Value.SubscriptionId, UserData);

                    return RedirectToPage("./Index");
                }
                else
                {
                    ErrorMessage = "Could not create new user";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }

        private async Task<UserData> GetUserDataAsync(int id)
        {
            try
            {
                UserData = await _userDataService.GetUserDataByIdAsync(_options.Value.SubscriptionId, id);
                return UserData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<Microsoft.Graph.User> CreateGraphUserAsync(Microsoft.Graph.User user)
        {
            try
            {
                Microsoft.Graph.User newUser = await _graphService.CreateUser(user);

                return newUser;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not add user : {user.DisplayName} to repository. {ex.Message}");
            }
        }
    }
}
