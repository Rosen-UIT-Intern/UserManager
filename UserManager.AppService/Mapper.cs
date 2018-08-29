using System;

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
                Email = JsonConvert.DeserializeObject<Email>(user.Email),
                Phone = JsonConvert.DeserializeObject<Phone>(user.Phone),
                Mobile = JsonConvert.DeserializeObject<Mobile>(user.Mobile)
            };
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
