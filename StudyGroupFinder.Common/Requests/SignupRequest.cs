using System;
namespace StudyGroupFinder.Common.Requests
{
    public class SignupRequest
    {
		public string Username { get; set; }
		public string Password { get; set; }
        public string Email { get; set; }
    }
}
