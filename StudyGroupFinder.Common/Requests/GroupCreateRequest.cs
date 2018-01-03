using System;
using StudyGroupFinder.Common.Responses;
using System.ComponentModel.DataAnnotations;

namespace StudyGroupFinder.Common.Requests
{
    public class CreateGroupRequest
    {
        [Required]
        public string Name { get; set; }

        public bool Private { get; set; }

        public int Size { get; set; }

    }
}
