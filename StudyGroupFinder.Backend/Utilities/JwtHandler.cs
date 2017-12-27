using System;
using System.Linq;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace StudyGroupFinder.Backend.Utilities
{
    public static class JwtHandler
    {
        public static SymmetricSecurityKey CreateSigningKey(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }

        public static JwtToken GenerateToken(JwtOptions options)
        {
            // Add registered claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, options.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, options.Id.ToString()),
            };

            // Add public claims
            claims.AddRange(options.PublicClaims.Select(x => new Claim(x.Key, x.Value)));

            // Create token
            var token = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(options.ExpiryInMinutes),
                signingCredentials: new SigningCredentials(
                    JwtHandler.CreateSigningKey(options.SecretKey),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtToken(token);
        }
    }
}
