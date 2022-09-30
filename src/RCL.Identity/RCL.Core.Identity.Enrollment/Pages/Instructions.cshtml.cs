using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace RCL.Core.Identity.Enrollment.Pages
{
    [AllowAnonymous]
    public class InstructionsModel : PageModel
    {
        private readonly IOptions<EnrollmentOptions> _options;
        public string ContactEmail { get; set; } = String.Empty;

        public InstructionsModel(IOptions<EnrollmentOptions> options)
        {
            _options = options;
        }

        public void OnGet()
        {
            ContactEmail = _options.Value.ContactEmail;
        }
    }
}
