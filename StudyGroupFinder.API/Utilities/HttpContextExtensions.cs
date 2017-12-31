using System;
using System.Security.Claims;

namespace StudyGroupFinder.API.Utilities
{
    public static class HttpContextExtensions
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return int.Parse(claimsPrincipal.FindFirstValue("jti"));
        }

        public static string GetUserEmail(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue("email");
        }

        public static string GetUserUsername(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue("username");
        }
    }
}
