using System;
using System.Collections.Generic;

namespace UserManager.Dal
{
    public class Group
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}