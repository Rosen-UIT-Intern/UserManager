using System;

namespace UserManager.Contract.DTOs
{
    public class CreateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }

        public Email[] Email { get; set; }
        public Phone[] WorkPhone { get; set; }
        public Phone[] PrivatePhone { get; set; }
        public Mobile[] Mobile { get; set; }

        public Guid OrganizationId { get; set; }

        public (Guid GroupId, bool isMain)[] Groups { get; set; }

        public (Guid RoleId, bool isMain)[] Roles { get; set; }
    }
}
