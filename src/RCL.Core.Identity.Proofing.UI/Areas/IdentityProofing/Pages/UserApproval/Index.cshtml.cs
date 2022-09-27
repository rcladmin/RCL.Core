using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace RCL.Core.Identity.Proofing.UI.Pages.UserApproval
{
    [Authorize(Policy = "UserAdmins")]
    public class IndexModel : PageModel
    {
        private readonly IUserDataService _userDataService;
        private readonly IOptions<IdentityProofingApiOptions> _options;

        public List<UserData> Users { get; set; } = new List<UserData>();
        public string ErrorMessage { get; set; } = string.Empty;

        public IndexModel(IUserDataService userDataService,
            IOptions<IdentityProofingApiOptions> options)
        {
            _userDataService = userDataService;
            _options = options;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Users = await _userDataService.GetUserDataBySubscriptionAsync(_options.Value.SubscriptionId);
            }
            catch(Exception ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
