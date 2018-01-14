using System;
using StudyGroupFinder.Common.Responses;
using System.ComponentModel.DataAnnotations;

namespace StudyGroupFinder.Common.Requests
{
    public class GroupJoinRequest
    {
        
        [Required]
        public int Group_Id { get; set; }

    }
}
