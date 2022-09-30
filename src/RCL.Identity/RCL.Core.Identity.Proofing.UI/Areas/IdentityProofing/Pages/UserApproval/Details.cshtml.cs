using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace RCL.Core.Identity.Proofing.UI.Pages.UserApproval
{
    [Authorize(Policy = "UserAdmins")]
    public class DetailsModel : PageModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IOptions<IdentityProofingApiOptions> _options;

        public UserData UserData { get; set; } = new UserData();
        public string ErrorMessage { get; set; } = string.Empty;

        public DetailsModel(IUserDataService userDataService, 
            IOptions<IdentityProofingApiOptions> options)
        {
            _userDataService = userDataService;
            _options = options;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            try
            {
                UserData = await _userDataService.GetUserDataByIdAsync(_options.Value.SubscriptionId, id);

                if (string.IsNullOrEmpty(UserData?.Email))
                {
                    ErrorMessage = "user not found";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
