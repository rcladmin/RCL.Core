using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RCL.Core.Identity.Proofing.UI.Test.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public IActionResult OnGet()
        {
            var claims = User.Claims;
            int x = claims.Count();

            return RedirectToPage("/UserApproval/Index", new { area = "IdentityProofing" });
        }
    }
}