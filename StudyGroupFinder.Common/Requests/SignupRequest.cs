using System;
using System.ComponentModel.DataAnnotations;

namespace StudyGroupFinder.Common.Requests
{
    public class SignupRequest
    {
        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Fname { get; set; }

        [Required]
        public string Lname { get; set; }
    }
}
