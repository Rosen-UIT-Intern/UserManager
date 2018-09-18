using System;

namespace UserManager.Contract.DTOs
{
    public class GroupDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OrganizationDTO Organization { get; set; }
        public bool IsMain { get; set; }
    }
}