using System;

namespace UserManager.Contract.DTOs
{
    public class FrontendUserDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }

        public Email[] Email { get; set; }
        public Phone[] WorkPhone { get; set; }
        public Phone[] PrivatePhone { get; set; }
        public Mobile[] Mobile { get; set; }

        public Guid OrganizationId { get; set; }

        public FrontendGroupDTO[] Groups { get; set; }

        public FrontendRoleDTO[] Roles { get; set; }
    }

    public class FrontendGroupDTO
    {
        public FrontendGroupDTO(Guid id, bool isMain)
        {
            Id = id;
            IsMain = isMain;
        }

        public Guid Id { get; set; }
        public bool IsMain { get; set; }
    }

    public class FrontendRoleDTO
    {
        public FrontendRoleDTO(Guid id, bool isMain)
        {
            Id = id;
            IsMain = isMain;
        }

        public Guid Id { get; set; }
        public bool IsMain { get; set; }
    }
}
