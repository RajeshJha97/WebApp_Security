using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebAPI.Models;
using WebAPI.Utilities;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private string secretKey;
        private Util util;
        public AuthController(IConfiguration config)
        {
            secretKey = config.GetValue<string>("SecretKey");
            util = new();
        }
        [HttpGet]
        public ActionResult GetConfig()
        {
            return Ok(secretKey);
        }

        [HttpPost("SignIn")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult SignIn([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new
                {
                    Error="Please provide the credentials"
                });
            }
            if (user.UserName == "admin" && user.Password == "password")
            {
                List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim("admin","")
                    };
                var expiresAt = DateTime.Now.AddMinutes(10);
                return Ok(new { 
                access_token=util.CreateToken(claims,expiresAt,secretKey),
                expires_At=expiresAt
                
                });

            }

            ModelState.AddModelError("Unauthorized", "Invalid username and password");
            return Unauthorized(ModelState);
        }
    }
}
