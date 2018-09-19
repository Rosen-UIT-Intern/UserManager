using System;
using System.Linq;
using Newtonsoft.Json;

using UserManager.Contract.DTOs;
using UserManager.Dal;

namespace UserManager.AppService.Utility
{
    public class Mapper
    {
        public static UserDTO MapFullWithoutResolvingGroupAndRole(User user)
        {
            return new UserDTO
            {
                Id = user.PersonalId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileImage = user.ProfileImage,
                Organization = Mapper.Map(user.Organization),
                Email = JsonConvert.DeserializeObject<Email[]>(user.Email),
                WorkPhone = JsonConvert.DeserializeObject<Phone[]>(user.WorkPhone),
                PrivatePhone = JsonConvert.DeserializeObject<Phone[]>(user.PrivatePhone),
                Mobile = JsonConvert.DeserializeObject<Mobile[]>(user.Mobile)
            };
        }

        public static UserDTO MapOnlyMainWithoutResolvingGroupAndRole(User user)
        {
            return new UserDTO
            {
                Id = user.PersonalId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileImage = user.ProfileImage,
                Organization = Mapper.Map(user.Organization),
                Email = JsonConvert.DeserializeObject<Email[]>(user.Email).Where(email => email.IsMain).ToArray(),
                WorkPhone = JsonConvert.DeserializeObject<Phone[]>(user.WorkPhone).Where(phone => phone.IsMain).ToArray(),
                PrivatePhone = JsonConvert.DeserializeObject<Phone[]>(user.PrivatePhone).Where(phone => phone.IsMain).ToArray(),
                Mobile = JsonConvert.DeserializeObject<Mobile[]>(user.Mobile).Where(mobile => mobile.IsMain).ToArray()
            };
        }

        [Obsolete("this method is not tested")]
        public static UserDTO Map(User user)
        {
            var userDTO = new UserDTO
            {
                Id = user.PersonalId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ProfileImage = user.ProfileImage,
                Organization = Mapper.Map(user.Organization),
                Email = JsonConvert.DeserializeObject<Email[]>(user.Email),
                WorkPhone = JsonConvert.DeserializeObject<Phone[]>(user.WorkPhone),
                PrivatePhone = JsonConvert.DeserializeObject<Phone[]>(user.PrivatePhone),
                Mobile = JsonConvert.DeserializeObject<Mobile[]>(user.Mobile)
            };

            var mainGroup = user.UserGroups.FirstOrDefault(usgr => usgr.IsMain);
            if (mainGroup == null)
            {

            }

            userDTO.Groups = user.UserGroups.Select(usgr =>
            {
                var grp = Mapper.Map(usgr.Group);
                grp.IsMain = usgr.IsMain;
                return grp;
            }).ToArray();

            var mainRole = user.UserRoles.FirstOrDefault(usrl => usrl.IsMain);
            if (mainRole == null)
            {

            }

            userDTO.Roles = user.UserRoles.Select(usrl =>
            {
                var role = Mapper.Map(usrl.Role);
                role.IsMain = usrl.IsMain;
                return role;
            }).ToArray();

            return userDTO;
        }

        public static GroupDTO Map(Group group)
        {
            return new GroupDTO()
            {
                Id = group.Id,
                Name = group.Name,
                Organization = Mapper.Map(group.Organization)
            };
        }

        public static Group Map(GroupDTO groupDTO)
        {
            return new Group()
            {
                Id = groupDTO.Id,
                Name = groupDTO.Name,
                OrganizationId = groupDTO.Organization.Id
            };
        }

        public static OrganizationDTO Map(Organization organization)
        {
            return new OrganizationDTO()
            {
                Id = organization.Id,
                Name = organization.Name
            };
        }

        public static Organization Map(OrganizationDTO organizationDTO)
        {
            return new Organization()
            {
                Id = organizationDTO.Id,
                Name = organizationDTO.Name
            };
        }

        public static RoleDTO Map(Role role)
        {
            return new RoleDTO()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static Role Map(RoleDTO roleDTO)
        {
            return new Role()
            {
                Id = roleDTO.Id,
                Name = roleDTO.Name
            };
        }
    }
}
