using System;

namespace UserManager.Dal
{
    public class UserGroup
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid GroupId { get; set; }
        public bool IsMain { get; set; }
    }
}