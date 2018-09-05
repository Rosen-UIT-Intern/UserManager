using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManager.Dal
{
    public class User
    {
        public string Id { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public string ProfileImage { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string WorkPhone { get; set; }
        [Required]
        public string PrivatePhone { get; set; }
        [Required]
        public string Mobile { get; set; }

        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}