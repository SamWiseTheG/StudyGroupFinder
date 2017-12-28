using System;
using System.Security.Claims;

namespace StudyGroupFinder.API.Utilities
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.FindFirstValue("jti"));
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue("email");
        }
    }
}
