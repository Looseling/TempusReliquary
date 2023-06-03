using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using TimeCapsuleBackend.Data.Models;

namespace TimeCapsuleBackend.Helper
{
    public class HelperFunctions
    {
        public static User GetCurrentUser(HttpContext httpContext)
        {
            var identity = httpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;
                return new User
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                };
            }

            return null;
        }
    }
}
