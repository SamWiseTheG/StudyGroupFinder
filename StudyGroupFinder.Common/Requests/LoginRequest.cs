using System;
using StudyGroupFinder.Common.Responses;

namespace StudyGroupFinder.Common.Requests
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
