using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages
{
    [Authorize(Policy = "MustBeFromHr")]
    //[Authorize]
    public class HumanResourceModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
