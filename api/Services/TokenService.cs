using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using api.interfaces;
using api.Models;

namespace api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;

        private readonly SymmetricSecurityKey _key;


        public TokenService(IConfiguration config)
        {
            _config = config;

            var signInKey = _config["JWT:SigninKey"] ?? throw new ArgumentNullException("JWT:SigninKey", "JWT SigninKey cannot be null.");

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signInKey));

        }

        public string CreateToken(AppUser user)
        {
            var email = user.Email ?? throw new ArgumentNullException("CreateToken", "AppUser Email cannot be null.");
            var username = user.UserName ?? throw new ArgumentNullException("CreateToken", "AppUser UserName cannot be null.");

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.GivenName, username),
            };

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Issuer = _config["JWT:Issuer"],
                Audience = _config["JWT:Audience"],
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
