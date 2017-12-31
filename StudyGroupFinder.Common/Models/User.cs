using System;
namespace StudyGroupFinder.Common.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Major { get; set; }
        public string Phone { get; set; }
    }
}
