using System;

namespace UserManager.Dal
{
    public class UserRole
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public bool IsMain { get; set; }
    }
}