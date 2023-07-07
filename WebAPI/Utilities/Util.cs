using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Utilities
{
    public class Util
    {
        public string CreateToken(List<Claim> clms, DateTime expires_At, string secretKey)
        {
            var key=Encoding.ASCII.GetBytes(secretKey);
            var jwt = new JwtSecurityToken(
                claims:clms,
                expires:expires_At,
                notBefore:DateTime.Now,
                signingCredentials:new SigningCredentials(
                    
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                    )                
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
