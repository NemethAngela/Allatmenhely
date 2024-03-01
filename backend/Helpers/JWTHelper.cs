using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Helpers
{
    public class JWTHelper
    {
        public static string GenerateToken(Admin adminUser, IConfiguration configuration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
            int.TryParse(configuration["Jwt:ExpirationHours"], out int exp);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Állatmenhely",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, adminUser.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(exp),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static bool ValidateToken(string token, string email, HttpContext context, IConfiguration configuration)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                var userEmail = jwtToken.Claims.First(x => x.Type == "email").Value;

                if (string.IsNullOrWhiteSpace(userEmail)) throw new UnauthorizedAccessException();

                var expirationEpochStr = jwtToken.Claims.First(x => x.Type == "exp").Value;
                long.TryParse(expirationEpochStr, out long expirationEpoch);
                var expiration = DateTimeOffset.FromUnixTimeSeconds(expirationEpoch).UtcDateTime;

                if (expiration.Subtract(DateTime.Now).TotalSeconds <= 0) throw new UnauthorizedAccessException();
            }
            catch
            {
                context.Response.StatusCode = 401;
                context.Response.WriteAsync("Unauthorized");
                return false;
            }

            return true;
        }
    }
}
