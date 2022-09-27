using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RCL.Core.Identity.Proofing.UI.Test.Pages
{
    public class IndexModel : PageModel
    {

        public IndexModel()
        {
        }

        public IActionResult OnGet()
        {
            return RedirectToPage("/UserApproval/Index", new { area = "IdentityProofing" });
        }
    }
}