using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace PocJwt
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public static class UserRepository
    {
        public static User Get()
        {
            return new User()
            {
                Username = "alan",
                Password = "1234"
            };
        }
    }

    public static class TokenService
    {
        public static string GenerateToken(User user)
        {
            var key = SecretKey.GetWithEncoding();
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

}