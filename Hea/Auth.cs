using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hea.Data;
using System.Text;
using System.Linq;
using Hea;
using Hea.Service;

namespace Hea
{
    public class Auth : IAuth
    {
        private readonly string key;
        private readonly Context context;

        public Auth(string key, Context context)
        {
            this.key = key;
            this.context = context;
        }

        public string Authentication(string username, string password)
        {
            var user = context.Users.SingleOrDefault(u => u.UserId.ToString() == username && u.Password == password);
            if (user == null)
                return null;

            // Create token handler
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // Create and return token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}