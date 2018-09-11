using System;
using System.Collections.Generic;
using System.Text;

namespace UserManager.Contract.DTOs
{
    public class QuerryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public Guid GroupId { get; set; }
        public string GroupName { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
