using System;

namespace UserManager.Dal
{
    public class Group
    {
        public Guid Id { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string Name { get; set; }
    }
}