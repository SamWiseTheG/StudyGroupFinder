using System;
namespace StudyGroupFinder.Common.Models
{
    public class GroupInvite
    {
        public int Group_Id { get; set; }
        public int User_Id { get; set; }
        public int? Inviter_Id { get; set; }
    }
}
