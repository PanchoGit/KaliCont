using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Kali.Domain;

namespace Kali.Security
{
    public class JwtBuilder
    {
        public UserToken Create(TokenProviderOptions options)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, options.NonceGenerator().Result),
                new Claim(JwtRegisteredClaimNames.Iat,
                    new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: options.Issuer,
                audience: options.Audience,
                claims: claims,
                notBefore: now,
                signingCredentials: options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new UserToken
            {
                AccessToken = encodedJwt
            };

            return response;
        }
    }
}
