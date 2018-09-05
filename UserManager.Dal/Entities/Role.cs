using System;
using System.Collections.Generic;

namespace UserManager.Dal
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}