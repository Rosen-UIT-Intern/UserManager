namespace UserManager.Contract.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }

        public Email Email { get; set; }
        public Phone Phone { get; set; }
        public Mobile Mobile { get; set; }

        public OrganizationDTO Organization { get; set; }

        public GroupDTO MainGroup { get; set; }
        public GroupDTO[] Groups { get; set; }

        public RoleDTO MainRole { get; set; }
        public RoleDTO[] Roles { get; set; }
    }

    public class Email
    {
        public string Main { get; set; }
        public string[] Emails { get; set; }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Email) && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 97;

                hash = hash * 89 + Main.GetHashCode();
                foreach (var email in Emails)
                {
                    hash = hash * 89 + email.GetHashCode();
                }

                return hash;
            }
        }
    }

    public class Phone
    {
        public string Main { get; set; }
        public string[] Work { get; set; }
        public string[] Private { get; set; }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Phone) && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 97;

                hash = hash * 89 + Main.GetHashCode();
                foreach (var phone in Work)
                {
                    hash = hash * 89 + phone.GetHashCode();
                }
                foreach (var phone in Private)
                {
                    hash = hash * 89 + phone.GetHashCode();
                }

                return hash;
            }
        }
    }

    public class Mobile
    {
        public string Main { get; set; }
        public string[] Mobiles { get; set; }

        public override bool Equals(object obj)
        {
            return obj.GetType() == typeof(Mobile) && obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 97;

                hash = hash * 89 + Main.GetHashCode();
                foreach (var mobile in Mobiles)
                {
                    hash = hash * 89 + mobile.GetHashCode();
                }

                return hash;
            }
        }
    }
}
