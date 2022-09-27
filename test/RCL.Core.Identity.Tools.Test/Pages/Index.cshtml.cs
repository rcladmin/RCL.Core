using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RCL.Core.Identity.Tools.Test.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }

        public void OnGet()
        {
        }
    }
}