using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using APIDemoProject.Data;
using Microsoft.IdentityModel.Tokens;

namespace APIDay1.Helpers
{
    public class TokenHelper
    {
        public static string GenerateToken(int userID, string name, string role)
        {
            var key = Encoding.UTF8.GetBytes(Constants.SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier , userID.ToString()),
                    new Claim(ClaimTypes.Role , role)
                }),
                Issuer = Constants.JWTIssuer,
                Audience = Constants.JWTAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Expires = DateTime.UtcNow.AddDays(2),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
