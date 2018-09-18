using System;
using System.ComponentModel.DataAnnotations;

namespace UserManager.Contract.DTOs
{
    public class FrontendUserDTO
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string ProfileImage { get; set; }

        [Required]
        public Email[] Email { get; set; }
        [Required]
        public Phone[] WorkPhone { get; set; }
        public Phone[] PrivatePhone { get; set; }
        [Required]
        public Mobile[] Mobile { get; set; }

        [Required]
        public Guid OrganizationId { get; set; }

        [Required]
        public FrontendGroupDTO[] Groups { get; set; }

        [Required]
        public FrontendRoleDTO[] Roles { get; set; }
    }

    public class FrontendGroupDTO
    {
        public FrontendGroupDTO(Guid id, bool isMain)
        {
            Id = id;
            IsMain = isMain;
        }

        [Required(ErrorMessage = "The Group's Id field is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Group's IsMain field is required")]
        public bool IsMain { get; set; }
    }

    public class FrontendRoleDTO
    {
        public FrontendRoleDTO(Guid id, bool isMain)
        {
            Id = id;
            IsMain = isMain;
        }

        [Required(ErrorMessage = "The Role's Id field is required")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The Role's IsMain field is required")]
        public bool IsMain { get; set; }
    }
}
