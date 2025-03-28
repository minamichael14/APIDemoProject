using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace APIDay1.Helpers
{
    public class TokenHelper
    {
        public static string GenerateToken(int userID, string name, string Role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("3eejjd46bfro83bhf8@fmr3$#cmdl18mkd");

            JwtSecurityToken myToken = new JwtSecurityToken(
                    issuer: "issuer",
                    audience: "",
                    expires: DateTime.Now.AddDays(1),
                    claims: new[]{
                        new Claim(ClaimTypes.Name, name),
                        new Claim("ID", userID.ToString()),
                        new Claim(ClaimTypes.Role, Role)
                    },
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                );
            //var tokenDescriptor= new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new[]
            //    {
            //        new Claim(ClaimTypes.Name, name),
            //        new Claim("ID", userID.ToString()),
            //        new Claim(ClaimTypes.Role, Role)
            //    }),
            //    Issuer = "issuer",
            //    Expires = DateTime.UtcNow.AddDays(1),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            //};

            //var token = tokenHandler.CreateToken(myToken);
            return tokenHandler.WriteToken(myToken);
        }
    }
}
