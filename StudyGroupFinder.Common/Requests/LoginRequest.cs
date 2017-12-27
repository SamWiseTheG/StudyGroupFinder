using System;
using StudyGroupFinder.Common.Responses;

namespace StudyGroupFinder.Common.Requests
{
    public class LoginRequest : BaseResponse
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
