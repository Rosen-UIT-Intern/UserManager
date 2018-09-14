using System;

namespace UserManager.Dal
{
    public class UserGroup
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public bool IsMain { get; set; }
    }
}