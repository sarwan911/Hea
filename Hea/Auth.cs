﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Hea.Data;
using System.Text;

namespace Hea
{
    public class Auth : IAuth
    {
        private readonly string key;
        private readonly Context _context;

        public Auth(string key, Context context)
        {
            this.key = key;
            _context = context;
        }

        public string Authentication(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId.ToString() == username && u.Password == password);
            if (user == null)
            {
                return null;
            }

            // 1. Create Security Token Handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // 2. Create Private Key to Encrypt
            var tokenKey = Encoding.ASCII.GetBytes(key);

            // 3. Create JWT descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // 4. Create Token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 5. Return Token from method
            return tokenHandler.WriteToken(token);
        }
    }
}