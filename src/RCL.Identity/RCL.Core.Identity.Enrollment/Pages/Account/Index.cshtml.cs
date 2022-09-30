using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RCL.Core.Identity.Tools;

namespace RCL.Core.Identity.Enrollment.Pages.Account
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public UserClaimsData UserData { get; set; } = new UserClaimsData();

        public void OnGet()
        {
            UserData = UserClaimsHelper.GetUserDataFromClaims(User);
        }
    }
}
