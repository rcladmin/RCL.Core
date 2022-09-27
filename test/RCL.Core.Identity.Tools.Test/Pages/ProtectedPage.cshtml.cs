#nullable disable

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RCL.Core.Identity.Tools.Test.Pages
{
    [Authorize(Policy = "UserAdmins")]
    public class ProtectedPageModel : PageModel
    {
        public UserClaimsData UserData { get; set; }

        public void OnGet()
        {
            UserData = UserClaimsHelper.GetUserDataFromClaims(User);
        }
    }
}
