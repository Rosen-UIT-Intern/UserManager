using System.ComponentModel.DataAnnotations;

namespace UserManager.Contract.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }

        public Email[] Email { get; set; }
        public Phone[] WorkPhone { get; set; }
        public Phone[] PrivatePhone { get; set; }
        public Mobile[] Mobile { get; set; }

        public OrganizationDTO Organization { get; set; }

        public GroupDTO MainGroup { get; set; }
        public GroupDTO[] Groups { get; set; }

        public RoleDTO MainRole { get; set; }
        public RoleDTO[] Roles { get; set; }
    }

    public class Email
    {
        [Required(ErrorMessage = "The Email's Address field is required")]
        [EmailAddress(ErrorMessage = "The Email's Address field must be of valid email address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Email's IsMain field is required")]
        public bool IsMain { get; set; }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Email) && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 97;

                hash = hash * 89 + Address.GetHashCode();
                hash = hash * 89 + IsMain.GetHashCode();

                return hash;
            }
        }
    }

    public class Phone
    {
        [Required(ErrorMessage = "The Phone's Number field is required")]
        [Phone(ErrorMessage = "The Phone's Number field must be of valid phone number")]
        public string Number { get; set; }
        [Required(ErrorMessage = "The Phone's IsMain field is required")]
        public bool IsMain { get; set; }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Phone) && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 97;

                hash = hash * 89 + Number.GetHashCode();
                hash = hash * 89 + IsMain.GetHashCode();

                return hash;
            }
        }
    }

    public class Mobile
    {
        [Required(ErrorMessage = "The Mobile's Number field is required")]
        [Phone]
        public string Number { get; set; }
        [Required(ErrorMessage = "The Mobile's IsMain field is required")]
        public bool IsMain { get; set; }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Mobile) && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 97;

                hash = hash * 89 + Number.GetHashCode();
                hash = hash * 89 + IsMain.GetHashCode();

                return hash;
            }
        }
    }
}
