using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Kali.Common.Security.Interfaces;

namespace Kali.Common.Security
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor contextAccessor;

        public SessionManager(IHttpContextAccessor contextAccessor = null)
        {
            this.contextAccessor = contextAccessor;
        }

        public int GetClientId()
        {
            if (contextAccessor?.HttpContext == null) return 0;

            var claims = contextAccessor.HttpContext.User.Claims;

            return int.Parse(claims.First(_ => _.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
