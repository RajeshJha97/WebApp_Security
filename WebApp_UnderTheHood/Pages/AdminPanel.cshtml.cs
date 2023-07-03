using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_UnderTheHood.Pages
{
    [Authorize(Policy ="AdminOnly")]
    public class AdminPanelModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
