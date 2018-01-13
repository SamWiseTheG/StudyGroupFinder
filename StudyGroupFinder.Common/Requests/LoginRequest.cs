using System;
using StudyGroupFinder.Common.Responses;

namespace StudyGroupFinder.Common.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
