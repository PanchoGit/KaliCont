using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace Kali.Security
{
    public class SecurityManager
    {
        public SecurityManager(TokenSetting tokenSetting)
        {
            var signingKey = new SymmetricSecurityKey(
                Encoding.ASCII
                    .GetBytes(tokenSetting.SecretKey));

            TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = tokenSetting.Issuer,
                ValidateAudience = true,
                ValidAudience = tokenSetting.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = false
            };

            TokenProviderOptions = new TokenProviderOptions
            {
                Audience = tokenSetting.Audience,
                Issuer = tokenSetting.Issuer,
                SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                IdentityResolver = GetIdentity
            };
        }

        public TokenValidationParameters TokenValidationParameters { get; }

        public TokenProviderOptions TokenProviderOptions { get; }

        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var loggedUser = username;

            if (loggedUser == null) return Task.FromResult<ClaimsIdentity>(null);

            var identity = new ClaimsIdentity("JWT");

            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));

            return Task.FromResult(identity);
        }
    }
}
