using System;
using StudyGroupFinder.Common.Responses;
using System.ComponentModel.DataAnnotations;

namespace StudyGroupFinder.Common.Requests
{
    public class GroupInviteRequest
    {
        [Required]
        public int User_Id { get; set; }

        [Required]
        public int Group_Id { get; set; }

        public int? Inviter_Id { get; set; }

    }
}
