using System;
using System.Collections.Generic;
using System.Text;

using Xunit;

using UserManager.AppService.Utility;
using UserManager.Contract.DTOs;
using UserManager.Dal;

namespace UserManager.AppService.Test
{
    public class TestMapper
    {
        [Fact]
        [Trait("Category", "Unit")]
        public void TestFromUserToLightUserDTO()
        {
            var testUser = SeedData.Instance.User;
            testUser.Organization = SeedData.Instance.RosenOrg;

            var testUserDTO = Mapper.MapLight(testUser);

            Assert.Equal(testUser.PersonalId, testUserDTO.Id);
            Assert.Equal(testUser.FirstName, testUserDTO.FirstName);
            Assert.Equal(testUser.LastName, testUserDTO.LastName);
            Assert.Equal(testUser.ProfileImage, testUserDTO.ProfileImage);

            Assert.Equal(testUser.Organization.Id, testUserDTO.Organization.Id);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void TestFromGroupToGroupDTO()
        {
            var groupId = Guid.NewGuid();
            var groupName = "Test Group";
            Group group = new Group()
            {
                Id = groupId,
                Name = groupName
            };
            var orgId = Guid.NewGuid();
            var orgName = "Test Org";
            group.Organization = new Organization()
            {
                Id = orgId,
                Name = orgName
            };
            GroupDTO dto = Mapper.Map(group);

            Assert.Equal(dto.Id, groupId);
            Assert.Equal(dto.Name, groupName);
            Assert.Equal(dto.Organization.Id, orgId);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void TestFromGroupDTOToGroup()
        {
            var groupId = Guid.NewGuid();
            var groupName = "Test Group";
            var orgId = Guid.NewGuid();
            GroupDTO groupDto = new GroupDTO()
            {
                Id = groupId,
                Name = groupName,
                Organization = new OrganizationDTO()
                {
                    Id = orgId
                }
            };
            Group group = Mapper.Map(groupDto);

            Assert.Equal(group.Id, groupId);
            Assert.Equal(group.Name, groupName);
            Assert.Equal(group.OrganizationId, orgId);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void TestFromOrganizationToOrganizationDTO()
        {
            var orgId = Guid.NewGuid();
            var orgName = "Test Organization";

            Organization org = new Organization()
            {
                Id = orgId,
                Name = orgName
            };

            OrganizationDTO orgDTO = Mapper.Map(org);

            Assert.Equal(orgDTO.Id, orgId);
            Assert.Equal(orgDTO.Name, orgName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void TestFromOrganizationDTOToOrganization()
        {
            var orgId = Guid.NewGuid();
            var orgName = "Test Organization";

            OrganizationDTO orgDTO = new OrganizationDTO()
            {
                Id = orgId,
                Name = orgName
            };

            Organization org = Mapper.Map(orgDTO);

            Assert.Equal(org.Id, orgId);
            Assert.Equal(org.Name, orgName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void TestFromRoleToRoleDTO()
        {
            var roleId = Guid.NewGuid();
            var roleName = "Test Role";

            Role role = new Role()
            {
                Id = roleId,
                Name = roleName
            };

            RoleDTO roleDTO = Mapper.Map(role);

            Assert.Equal(roleDTO.Id, roleId);
            Assert.Equal(roleDTO.Name, roleName);
        }

        [Fact]
        [Trait("Category", "Unit")]
        public void TestFromRoleDTOToRole()
        {
            var roleId = Guid.NewGuid();
            var roleName = "Test Role";

            RoleDTO roleDTO = new RoleDTO()
            {
                Id = roleId,
                Name = roleName
            };

            Role role = Mapper.Map(roleDTO);

            Assert.Equal(role.Id, roleId);
            Assert.Equal(role.Name, roleName);
        }
    }
}
