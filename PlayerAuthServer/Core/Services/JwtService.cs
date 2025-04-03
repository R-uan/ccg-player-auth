using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlayerAuthServer.Core.Interfaces;
using PlayerAuthServer.Database.Entities;
using PlayerAuthServer.Utilities;

namespace PlayerAuthServer.Core.Services
{
    public class JwtService(IOptions<JwtSettings> jwtSettings) : IJwtService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        public string GenerateToken(Player player)
        {
            byte[] signingKey = Encoding.UTF8.GetBytes(_jwtSettings.SigningKey);
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(signingKey),
                SecurityAlgorithms.HmacSha256Signature
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                SigningCredentials = signingCredentials,
                Subject = GenerateIdentity(player),
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateIdentity(Player player)
        {
            var claimIdentity = new ClaimsIdentity();
            var emailClaim = new Claim(ClaimTypes.Email, player.Email);
            var uuidClaim = new Claim("UUID", player.UUID.ToString());
            claimIdentity.AddClaims([emailClaim, uuidClaim]);
            return claimIdentity;
        }
    }
}