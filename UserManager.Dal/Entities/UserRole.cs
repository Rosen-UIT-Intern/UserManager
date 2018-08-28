using System;

namespace UserManager.Dal
{
    public class UserRole
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Guid RoleId { get; set; }
        public bool IsMain { get; set; }
    }
}