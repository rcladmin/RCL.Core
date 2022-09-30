using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace RCL.Core.Identity.Enrollment.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly IOptions<EnrollmentOptions> _options;
        public string ContactEmail { get; set; } = String.Empty;

        public IndexModel(IOptions<EnrollmentOptions> options)
        {
            _options = options;
        }

        public IActionResult OnGet()
        {
            ContactEmail = _options.Value.ContactEmail;
            
            if(!string.IsNullOrEmpty(User?.Identity?.Name))
            {
                return RedirectToPage("/Account/Index", new { area = "" });
            }

            return Page();
        }
    }
}