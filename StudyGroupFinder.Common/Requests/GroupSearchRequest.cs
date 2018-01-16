using System;
using StudyGroupFinder.Common.Responses;
using System.ComponentModel.DataAnnotations;

namespace StudyGroupFinder.Common.Requests
{
    public class GroupSearchRequest
    {
        
        [Required]
        public string Name { get; set; }

    }
}
