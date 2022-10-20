using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace RCL.Core.Identity.Proofing.UI.Areas.IdentityProofing.Pages.IdentityApprover
{
    [Authorize(Policy = "UserAdmins")]
    public class DetailsModel : PageModel
    {
        private readonly IIdentityApproverService _identityApproverService;
        private readonly IOptions<IdentityProofingApiOptions> _options;

        public Proofing.IdentityApprover IdentityApprover { get; set; } = new Proofing.IdentityApprover();
        public string ErrorMessage { get; set; } = string.Empty;

        public DetailsModel(IIdentityApproverService identityApproverService, IOptions<IdentityProofingApiOptions> options)
        {
            _identityApproverService = identityApproverService;
            _options = options;           
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                IdentityApprover = await _identityApproverService
                    .GetIdentityApproverByIdentifierAsync(_options.Value.SubscriptionId, _options.Value.IdentityApproverIdentifier);

                if (string.IsNullOrEmpty(IdentityApprover?.Name))
                {
                    ErrorMessage = "An Identity Approver was not found or registered for this application.";
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
