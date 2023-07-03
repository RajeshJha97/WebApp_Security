using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;

namespace WebApp_UnderTheHood.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential credential { get; set; }
        public void OnGet()
        {
            //this.credential = new Credential {UserName="Admin" };
        }
        public async Task<IActionResult> OnPostAsync() 
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (credential.UserName == "admin" && credential.Password == "password")
            {
                //creating security context
                //claim
                List<Claim> claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Name,"admin"),
                    new Claim(ClaimTypes.Email,"admin@email.com"),
                    new Claim("Department","Hr"),
                    new Claim("Admin","true"),
                    new Claim("Manager","true")
                };
                //create an Identity and add that claims into that one
                ClaimsIdentity identiy= new ClaimsIdentity(claims,"MyCookieAuthType"); //object required 2 -param first is claims list and another is auth type
                //Adding Identity into principal
                ClaimsPrincipal principal= new ClaimsPrincipal(identiy);

                //Implememnting persistent cookie
                var authProperties = new AuthenticationProperties()
                {
                    IsPersistent = credential.RememberMe
                };

                //As principal hold the security context we have to serialize it
                await HttpContext.SignInAsync(principal,authProperties);

                //redirecting to the login page
                return RedirectToPage("/Index");


            }
            return Page();
        }
    }

    public class Credential
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }
    }
}
