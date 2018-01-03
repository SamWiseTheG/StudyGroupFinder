using System;
namespace StudyGroupFinder.Common.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Private { get; set; }
        public int Size { get; set; }
    }
}
