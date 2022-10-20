#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using RCL.Core.Identity.Graph;
using System.Text.Json;

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

                    return Page();
                }

                bool b = await IsUserDuplicateAsync(UserData);

                if(b == true)
                {
                    ErrorMessage = "A duplicate user exists with the same name and date of birth.";

                    return Page();
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

                    return Page();
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

        private async Task<bool> IsUserDuplicateAsync(UserData userData)
        {
            bool b = false;

            try
            {
                List<Microsoft.Graph.User> users = await _graphService.GetUsersByNameAsync(userData.GivenName, userData.SurName);

                if(users.Count > 0)
                {
                    foreach(var user in users)
                    {
                        Dictionary<string, object> customAttributes = _graphService.GetUserCustomAttributes(user);

                        if (customAttributes?.Count > 0)
                        {
                            JsonElement dateOfBirth = (JsonElement)customAttributes[$"extension_{userData.B2CExtensionAppId}_DateofBirth"];

                            if (!string.IsNullOrEmpty(dateOfBirth.GetString()))
                            {
                                DateTime dob = DateTime.ParseExact(dateOfBirth.GetString(), "dd/MM/yyyy", null);

                                if (dob == userData.DateOfBirth)
                                {
                                    return true;
                                }
                                
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return b;
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
