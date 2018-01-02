using System;
using StudyGroupFinder.Common.Responses;
using System.ComponentModel.DataAnnotations;

namespace StudyGroupFinder.Common.Requests
{
    public class GroupInviteRequest
    {
        [Required]
        public string User_Id { get; set; }

        [Required]
        public string Group_Id { get; set; }

        public string Inviter_Id { get; set; }

    }
}
