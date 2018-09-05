using System;
using System.Linq;
using Newtonsoft.Json;

using UserManager.Contract.DTOs;
using UserManager.Dal;

namespace UserManager.AppService.Utility
{
    public class Mapper
    {
        public static UserDTO MapLight(User user)
        {


            return new UserDTO
            {
                Id = user.Id,
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

        [Obsolete("this method is not tested")]
        public static UserDTO Map(User user)
        {

            var userDTO = new UserDTO
            {
                Id = user.Id,
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

            userDTO.MainGroup = Mapper.Map(mainGroup.Group);
            userDTO.Groups = user.UserGroups.Select(usgr => Mapper.Map(usgr.Group)).ToArray();

            var mainRole = user.UserRoles.FirstOrDefault(usrl => usrl.IsMain);
            if (mainRole == null)
            {

            }

            userDTO.MainRole = Mapper.Map(mainRole.Role);
            userDTO.Roles = user.UserRoles.Select(usrl => Mapper.Map(usrl.Role)).ToArray();

            return userDTO;
        }

        public static GroupDTO Map(Group group)
        {
            return new GroupDTO()
            {
                Id = group.Id.ToString(),
                Name = group.Name
            };
        }

        public static Group Map(GroupDTO groupDTO, Guid organizationId)
        {
            return new Group()
            {
                Id = Guid.Parse(groupDTO.Id),
                Name = groupDTO.Name,
                OrganizationId = organizationId
            };
        }

        public static OrganizationDTO Map(Organization organization)
        {
            return new OrganizationDTO()
            {
                Id = organization.Id.ToString(),
                Name = organization.Name
            };
        }

        public static Organization Map(OrganizationDTO organizationDTO)
        {
            return new Organization()
            {
                Id = Guid.Parse(organizationDTO.Id),
                Name = organizationDTO.Name
            };
        }

        public static RoleDTO Map(Role role)
        {
            return new RoleDTO()
            {
                Id = role.Id.ToString(),
                Name = role.Name
            };
        }

        public static Role Map(RoleDTO roleDTO)
        {
            return new Role()
            {
                Id = Guid.Parse(roleDTO.Id),
                Name = roleDTO.Name
            };
        }
    }
}
