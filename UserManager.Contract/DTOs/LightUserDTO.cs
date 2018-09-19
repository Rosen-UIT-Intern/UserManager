using System;
using System.Collections.Generic;
using System.Text;

namespace UserManager.Contract.DTOs
{
    public class LightUserDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }

        public Email MainEmail { get; set; }
        public Phone MainWorkPhone { get; set; }
        public Phone MainPrivatePhone { get; set; }
        public Mobile MainMobile { get; set; }

        public OrganizationDTO Organization { get; set; }

        public GroupDTO MainGroup { get; set; }

        public RoleDTO MainRole { get; set; }
    }
}
